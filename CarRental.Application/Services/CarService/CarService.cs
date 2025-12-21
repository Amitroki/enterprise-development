using MapsterMapper;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Infrastructure.InMemoryRepository;

namespace CarRental.Application.Services.CarService;

public class CarService : IApplicationService<CarDto, CarCreateUpdateDto>
{
	private readonly CarRepository _carRepo;
	private readonly CarModelGenerationRepository _modelRepo;
	private readonly IMapper _mapper;

	public CarService(CarRepository carRepo, CarModelGenerationRepository modelRepo, IMapper mapper)
	{
		_carRepo = carRepo;
		_modelRepo = modelRepo;
		_mapper = mapper;
	}

	public CarDto Create(CarCreateUpdateDto dto)
	{
		var car = _mapper.Map<Car>(dto);

		car.ModelGeneration = _modelRepo.Read(dto.ModelGenerationId)
			?? throw new Exception("Model not found");

		var newId = _carRepo.Create(car);

		return _mapper.Map<CarDto>(_carRepo.Read(newId));
	}

	public List<CarDto> ReadAll()
	{
		var cars = _carRepo.ReadAll();
		return _mapper.Map<List<CarDto>>(cars);
	}

	public CarDto? Read(uint id)
	{
		var car = _carRepo.Read(id);
		return car == null ? null : _mapper.Map<CarDto>(car);
	}

	public CarDto Update(CarCreateUpdateDto dto, uint id)
	{
		var car = _mapper.Map<Car>(dto);
		car.Id = id;
		car.ModelGeneration = _modelRepo.Read(dto.ModelGenerationId)
			?? throw new Exception("Model not found");

		_carRepo.Update(car, id);
		return _mapper.Map<CarDto>(car);
	}

	public bool Delete(uint id) => _carRepo.Delete(id);
}