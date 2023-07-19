using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new ()
        {
            new VillaDTO {Id = 1, Name = "poool_view", Occupancy = 111, Sqft = 11},
            new VillaDTO {Id = 2, Name = "beach_view", Occupancy = 222, Sqft = 22}
        };
    }
}
