using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LendCar.Pages
{
    //[Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private ICarRepository _carRepository;
        private IBrandRepository _brandRepo;
        private IBrandModelRepository _brandModelRepo;
        private IVehicleTypeRepository _vehicleTypeRepo;
        private IColorRepository _ColorRepo;

        public IPagedList<Vehicle> Vehicles { get; set; }
        public IPagedList<Brand> Brands { get; set; }
        public IPagedList<BrandModel> BrandModels { get; set; }
        public IPagedList<VehicleType> VehicleTypes { get; set; }
        public IPagedList<Color> Colors { get; set; }

        public SelectList BrandsSelectList { get; set; }
        public int CurrentVehiclesPage { get; set; }
        public int CurrentBrandsPage { get; set; }
        public int CurrentBrandModelsPage { get; set; }
        public int CurrentVTypesPage { get; set; }
        public int CurrentColorsPage { get; set; }
        public Color NewColor { get; set; }
        public int PageSize { get; set; }
        public AdminModel(ICarRepository carRepository,
                          IBrandRepository brandRepository,
                          IBrandModelRepository brandModelRepository,
                          IVehicleTypeRepository vehicleTypeRepository,
                          IColorRepository colorRepository) 
        {
            this._carRepository = carRepository;
            this._brandRepo = brandRepository;
            this._brandModelRepo = brandModelRepository;
            this._vehicleTypeRepo = vehicleTypeRepository;
            this._ColorRepo = colorRepository;
            this.NewColor = new Color();
        }
        public void OnGet()
        {
            BrandsSelectList = new SelectList(_brandRepo.GetAllBrands().OrderBy(b=>b.Name),"Id","Name");
            Request.Query.TryGetValue("entries", out var entries);
            Request.Query.TryGetValue("vehiclePage", out var vehiclePage);
            Request.Query.TryGetValue("VTypePage", out var VTypePage);
            Request.Query.TryGetValue("brandPage", out var brandPage);
            Request.Query.TryGetValue("brandModelPage", out var brandModelPage);
            Request.Query.TryGetValue("colorPage", out var colorPage);

            int vehiclesPageNumber = 1;
            int vTypePageNumber = 1;
            int brandsPageNumber = 1;
            int brandModelsPageNumber = 1;
            int colorPageNumber = 1;

            if (entries.Count > 0)
            {
                if (int.TryParse(entries[0], out var _pageSize))
                    PageSize = _pageSize;
            }
            else
                PageSize = 5;

            if (vehiclePage.Count > 0)
                if (int.TryParse(vehiclePage[0], out var pageNum))
                    vehiclesPageNumber = pageNum;

            if (brandPage.Count > 0)
                if (int.TryParse(brandPage[0], out var pageNum))
                    brandsPageNumber = pageNum;
       
            if (brandModelPage.Count > 0)
                if (int.TryParse(brandModelPage[0], out var pageNum))
                    brandModelsPageNumber = pageNum;

            if (VTypePage.Count > 0)
                if (int.TryParse(VTypePage[0], out var pageNum))
                    vTypePageNumber = pageNum;

            if (colorPage.Count > 0)
                if (int.TryParse(colorPage[0], out var pageNum))
                    colorPageNumber = pageNum;


            CurrentVehiclesPage = vehiclesPageNumber;
            CurrentBrandsPage = brandsPageNumber;
            CurrentBrandModelsPage = brandModelsPageNumber;
            CurrentVTypesPage = vTypePageNumber;
            CurrentColorsPage = colorPageNumber;

            Vehicles = _carRepository.GetAllVehiclesRequests().ToList().ToPagedList(vehiclesPageNumber, PageSize);
            Brands = _brandRepo.GetAllBrands().ToList().ToPagedList(brandsPageNumber, PageSize);
            BrandModels = _brandModelRepo.GetAllBrandModels().ToList().ToPagedList(brandModelsPageNumber, PageSize);
            VehicleTypes = _vehicleTypeRepo.GetAllVehicleTypes().ToList().ToPagedList(vTypePageNumber, PageSize);
            Colors = _ColorRepo.GetAllColors().ToList().ToPagedList(colorPageNumber,PageSize);
        }
    }
}