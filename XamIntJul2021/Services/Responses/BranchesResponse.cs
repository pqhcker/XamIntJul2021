using System;
using System.Collections;
using System.Collections.Generic;
using XamIntJul2021.AppBase.Services.Reponses;
using XamIntJul2021.Model;

namespace XamIntJul2021.Services.Responses
{
    public class BranchesResponse : BaseServiceResponse
    {
       public IEnumerable<Branch> Branches { get; set; }
    }
}
