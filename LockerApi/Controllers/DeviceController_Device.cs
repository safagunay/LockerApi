﻿using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace LockerApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Device")]
    public partial class DeviceController : ApiController
    {
        private readonly QRCodeService _qrCodeService = new QRCodeService();
        private readonly DeviceService _deviceService = new DeviceService();
        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        //POST api/Device/RegisterDevice
        [Route("RegisterDevice")]
        public IHttpActionResult RegisterDevice(RegisterDeviceBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (!UserManager.FindById(userId).EmailConfirmed)
            {
                ModelState.AddModelError("Email", "User email is not confirmed.");
                return BadRequest(ModelState);
            }
            var device = _deviceService.getByCodeHash(model.DeviceCode);
            if (device != null && device.User_Id == null)
            {

                device.User_Id = userId;
                device.Name = model.Name;
                device.Code = model.DeviceCode;
                device.RegisteredOnUTC = DateService.getCurrentUTC();
                _deviceService.updateDevice(device);
                return Ok();
            }
            ModelState.AddModelError("DeviceCode", "Invalid device code.");
            return BadRequest(ModelState);
        }

        //POST api/Device/UpdateDeviceName
        [Route("UpdateDeviceName")]
        public IHttpActionResult UpdateDeviceName(UpdateDeviceNameBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            var device = _deviceService.getByCode(model.DeviceCode);
            if (device != null && device.User_Id == userId)
            {
                _deviceService.updateDeviceName(device.Id, model.Name);
                return Ok();
            }
            ModelState.AddModelError("DeviceCode", "Invalid device code.");
            return BadRequest(ModelState);
        }

        //Get api/Device/RegisteredDevices
        [Route("RegisteredDevices")]
        public IEnumerable<DeviceDTO> GetRegisteredDevices()
        {
            var userId = User.Identity.GetUserId();
            var devices = _deviceService.getRegisteredDevicesOf(userId);
            var deviceDTOs = new List<DeviceDTO>();
            foreach (var device in devices)
            {
                deviceDTOs.Add
                    (
                        new DeviceDTO()
                        {
                            Name = device.Name,
                            DeviceCode = device.Code,
                            RegisteredOnUTC = device.RegisteredOnUTC.Value
                        }

                    );
            }
            return deviceDTOs;
        }
    }

}
