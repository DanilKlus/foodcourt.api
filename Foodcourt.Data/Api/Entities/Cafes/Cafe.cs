﻿using Foodcourt.Data.Api.Entities.Orders;
using Foodcourt.Data.Api.Entities.Users;

namespace Foodcourt.Data.Api.Entities.Cafes;

public class Cafe
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CafeStatus Status { get; set; }
    public string Avatar { get; set; }
    public string Location { get; set; }
    public bool IsActive { get; set; }
    
    public List<Order> Orders { get; set; }
    public List<Product> Products { get; set; }
    
    public virtual List<User> Users { get; set; } 
    
    public string CertifyingDocument { get; set; }
    public string Response { get; set; }
    public string PersonalAccount { get; set; }
    
}