using System;
using AutoMapper;
using NetTopologySuite.Geometries;
using OPS_API.Domain.Models;
using OPS_API.Domain.Models.Owned;
using OPS_API.Resources;

namespace OPS_API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DateTime, string>().ConvertUsing(v => v.ToString("s"));
            CreateMap<MapCoordinates, Point>()
                .ConvertUsing(mc => new Point(mc.Longitude, mc.Latitude));
            CreateMap<Point, MapCoordinates>()
                .ConvertUsing(p => MapCoordinates.FromPoint(p));

            CreateMap<Rescuer, RescuerResource>()
                .ForMember(
                    op => op.Inventory,
                    x => x.MapFrom(o => o.Inventory
                        .ConvertAll(n => n.EquipmentRequest.Equipment))
                );
            CreateMap<Operation, OperationResource>()
                .ForMember(
                    op => op.RequestedEquipment,
                    x => x.MapFrom(o => o.RequestedEquipment
                        .ConvertAll(n => n.Equipment))
                );

            CreateMap<Organization, OrganizationResource>();
            CreateMap<MissingPerson, MissingPersonDetails>();
            CreateMap<MissingPersonDetails, MissingPerson>();

            CreateMap<MissingPersonDocument, Operation>();
            CreateMap<JoinOperationResource, Rescuer>();

            CreateMap<RegistrationResource, User>();
            CreateMap<User, UserResource>();
        }
    }
}