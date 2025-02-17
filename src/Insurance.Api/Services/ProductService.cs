﻿using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
using Insurance.Domain.DomainExceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace Insurance.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient { BaseAddress = new Uri(_configuration["ProductApi:URL"]) };

        }
        public async Task<List<ProductDto>> GetProducts(IEnumerable<int> productsIds)
        {
            if((productsIds == null) || (!productsIds.Any()) || productsIds.Any(p => p == 0))
                throw new InvalidProductException("Invalid ProductId List");
            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProducts"]));
            var productDtoList = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(result.Content.ReadAsStringAsync().Result);
            var filteredProductDtoList = productDtoList.Where(pdto => productsIds.Any(productsIds => pdto.Id== productsIds)).ToList();
            return filteredProductDtoList;
        }
        public async Task<List<ProductTypeDto>> GetProductTypes(IEnumerable<ProductDto> productDtoList)
        {
            if ((productDtoList == null) || (!productDtoList.Any()) || productDtoList.Any(pdto => pdto.ProductTypeId == 0))
                throw new InvalidProductException("Invalid ProductDto List");
            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProductTypes"]));
            var productTypeDtoList = JsonConvert.DeserializeObject<IEnumerable<ProductTypeDto>>(result.Content.ReadAsStringAsync().Result);
            var filteredProductTypeDtoList = productTypeDtoList.Where(ptdto => productDtoList.Any(pdto => ptdto.Id == pdto.ProductTypeId)).ToList();
            return filteredProductTypeDtoList;
        }
    }
}
