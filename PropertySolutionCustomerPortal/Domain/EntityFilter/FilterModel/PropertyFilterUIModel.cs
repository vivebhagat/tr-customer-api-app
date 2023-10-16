using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel
{
    public class PropertyFilterUIModel
    {
        public string? Name { get; set; }
        public PropertyType? Type { get; set; }
        public int? Bedrooms { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public double? MinArea { get; set; }
        public double? MaxArea { get; set; }
        public int? Bathrooms { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
