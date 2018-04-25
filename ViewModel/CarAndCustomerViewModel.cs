using System;
using System.Security.Cryptography.X509Certificates;
using SammysAuto.Models;
using System.Collections.Generic;
namespace SammysAuto.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public ApplicationUser UserObj { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
