using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProducts(string token);
        Task<ProductModel> FindProductById(long id, string token);
        Task<ProductModel> CreateProduct(ProductModel productModel, string token);
        Task<ProductModel> UpdateProduct(ProductModel productModel, string token);
        Task<bool> DeleteProduct(long id, string token);
    }
}
