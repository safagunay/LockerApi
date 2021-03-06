﻿using LockerApi.Models;
using LockerApi.Services.Repositories;
using System.Collections.Generic;

namespace LockerApi.Services
{
    public class DeviceService
    {
        public Device getByCode(string code)
        {
            return DeviceRepository.getByCode(code);
        }

        public Device getByCodeHash(string code)
        {
            return DeviceRepository.getByCodeHash(
                HashService.HashDeviceCode(code));
        }

        public Device getBySecretHash(string secret)
        {
            return DeviceRepository.getBySecretHash(
                HashService.HashDeviceSecret(secret));
        }

        public void updateDeviceName(int deviceId, string name)
        {
            DeviceRepository.setName(deviceId, name);
        }

        public void updateDevice(Device device)
        {
            DeviceRepository.update(device);
        }
        public IEnumerable<Device> getRegisteredDevicesOf(string userId)
        {
            return DeviceRepository.getByUserId(userId);
        }

        public void AddOrUpdatePermission(DevicePermission permission)
        {
            DevicePermissionsRepository.InsertOrUpdate(permission);
        }

        public DevicePermission GetPermission(string userId, int deviceId)
        {
            var permission = DevicePermissionsRepository.GetByDeviceAndUserId(deviceId, userId);
            if (permission == null || DateService.isExpiredUTC(permission.ExpiresOnUTC))
                return null;
            return permission;

        }

        public void AddPermissionRecord(DevicePermissionRecord record)
        {
            DevicePermissionsRepository.InsertPermissionRecord(record);
        }

        public DevicePermission DeletePermission(int deviceId, string userId)
        {
            return DevicePermissionsRepository.Delete(deviceId, userId);
        }

        public IEnumerable<DevicePermission> getDevicePermissionList(int deviceId)
        {
            return DevicePermissionsRepository.GetByDeviceId(deviceId);
        }

        public IEnumerable<DevicePermission> GetAcquiredDevicePermissionList(string userId)
        {
            return DevicePermissionsRepository.GetByUserId(userId);
        }

        public Device GetById(int device_Id)
        {
            return DeviceRepository.getById(device_Id);
        }
    }
}