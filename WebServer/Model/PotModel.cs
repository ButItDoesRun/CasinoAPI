﻿namespace WebServer.Model
{
    public class PotModel
    {
        public double? PotAmount { get; set; }
        public UrlModel? CreatePotUrl { get; set; }
        public UrlModel? UpdatePotUrl { get; set; }
        public UrlModel? DeletePotUrl { get; set; }
    }

    public class PotUpdateModel
    {
        public double? Amount { get; set; }
    }

    public class PotCreateModel
    {
        public double? Amount { get; set; }
    }
}
