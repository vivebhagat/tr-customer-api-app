using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel;
using System.Linq;

namespace PropertySolutionCustomerPortal.Domain.EntityFilter
{
    public interface IPropertyFilter
    {
        IQueryable<Property> ApplyFilter(IQueryable<Property> properties, PropertyFilterUIModel filterUIModel);
    }

    public class PropertyFilter : IPropertyFilter
    {
        public IQueryable<Property> ApplyFilter(IQueryable<Property> properties, PropertyFilterUIModel filterUIModel)
        {
            if (!string.IsNullOrWhiteSpace(filterUIModel.Name))
                properties = properties.Where(p => p.Address.Contains(filterUIModel.Name));

            if (filterUIModel.Type.HasValue)
                properties = properties.Where(p => p.Type == filterUIModel.Type.Value);

            if (filterUIModel.Bedrooms.HasValue)
            {
                if (filterUIModel.Bathrooms.Value == 4)
                    properties = properties.Where(p => p.Bedrooms >= filterUIModel.Bedrooms.Value);
                else
                    properties = properties.Where(p => p.Bedrooms == filterUIModel.Bedrooms.Value);
            }

            if (filterUIModel.Bathrooms.HasValue)
            {
                if(filterUIModel.Bathrooms.Value == 4 )
                    properties = properties.Where(p => p.Bathrooms >= filterUIModel.Bathrooms.Value);
                else
                    properties = properties.Where(p => p.Bathrooms == filterUIModel.Bathrooms.Value);
            }

            if (filterUIModel.MinPrice.HasValue)
                properties = properties.Where(p => p.Price >= filterUIModel.MinPrice.Value);


            if (filterUIModel.MaxPrice.HasValue)
                properties = properties.Where(p => p.Price <= filterUIModel.MaxPrice.Value);

            if (filterUIModel.MinArea.HasValue)
                properties = properties.Where(p => p.Area <= filterUIModel.MinArea.Value);

            if (filterUIModel.MaxArea.HasValue)
                properties = properties.Where(p => p.Area <= filterUIModel.MaxArea.Value);

            properties = properties.Where(m => m.ArchiveDate == null);
            properties = properties.OrderByDescending(m => m.Id);

            return properties;
        }
    }

}
