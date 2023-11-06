﻿namespace Entities.RequestFeatures
{
    public class PlayerParameters : RequestParameters
    {
        public PlayerParameters()
        {
            OrderBy = "name";
        }

        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxAge > MinAge;
        public string SearchTerm { get; set; }

    }
}
