﻿namespace Insurance.Api.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SalesPrice { get; set; }
        public int ProductTypeId { get; set; }   
    }
}
