﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProducts(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel> FindProductById(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductViewModel>();
        }
        public async Task<ProductViewModel> CreateProduct(ProductViewModel productModel, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson(BasePath, productModel);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductViewModel>();
            else throw new Exception("Algo de errado na chamada da api:");
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productModel, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJson(BasePath, productModel);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductViewModel>();
            else throw new Exception("Algo de errado na chamada da api:");
        }
        public async Task<bool> DeleteProduct(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else 
                throw new Exception("Algo de errado aconteceu na chamada da API");
        }
    }
}
