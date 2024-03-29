﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcServiceCRUD.DataAccess
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
