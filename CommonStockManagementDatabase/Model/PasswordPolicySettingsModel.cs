﻿namespace CommonStockManagementDatabase.Model
{
    public class PasswordPolicySettingsModel
    {
        public int RequiredLength { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
    }
}
