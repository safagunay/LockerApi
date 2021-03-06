﻿using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class RegisterDeviceBindingModel
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string DeviceCode { get; set; }
    }
}