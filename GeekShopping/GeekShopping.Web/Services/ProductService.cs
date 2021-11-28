using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;

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

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContextAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContextAs<ProductModel>();
        }
        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            var response = await _httpClient.PostAsJson(BasePath, productModel);
            if (response.IsSuccessStatusCode) return await response.ReadContextAs<ProductModel>();
            else throw new Exception("Algo de errado na chamada da api:");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel productModel)
        {
            var response = await _httpClient.PutAsJson(BasePath, productModel);
            if (response.IsSuccessStatusCode) return await response.ReadContextAs<ProductModel>();
            else throw new Exception("Algo de errado na chamada da api:");
        }
        public async Task<bool> DeleteProduct(long id)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContextAs<bool>();
            else 
                throw new Exception("Algo de errado aconteceu na chamada da API");
        }
    }
}
