﻿using System;
using System.Collections.Generic;

namespace CrmDeveloperExtensions.Core.Models
{
    public class CrmDevExConfigOrgMap
    {
        public Guid OrganizationId { get; set; }
        public string ProjectUniqueName { get; set; }
        public List<CrmDexExConfigWebResource> WebResources { get; set; }
    }
}