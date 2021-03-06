﻿namespace LockerApi.Services
{
    public static class SettingsService
    {
        public const int ConfirmationTokenLength = 4;
        //In minutes
        public const int ConfirmationTokenDuration = 20;
        //In numbers
        public const int ConfirmationTokenTableCleanUpPeriod = 50;
        //In numbers
        public const int DevicePermissionsTableCleanUpPeriod = 50;
        //In Seconds
        public const int QRCodeDuration = 60;
        //In minutes
        public const int QRCodeTableCleanUpPeriod = 10;
    }
}