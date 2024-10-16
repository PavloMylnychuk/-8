using System;

// Завдання 1 і 2
abstract class Vehicle
{
    public string Brand { get; protected set; }
    public string Model { get; protected set; }
    public double EngineCapacity { get; protected set; }
    public double FuelConsumption { get; protected set; }
    public double FuelTankCapacity { get; protected set; }
    public double CurrentFuelAmount { get; protected set; }

    protected Vehicle(string brand, string model, double engineCapacity, double fuelConsumption, double fuelTankCapacity)
    {
        Brand = brand;
        Model = model;
        EngineCapacity = engineCapacity;
        FuelConsumption = fuelConsumption;
        FuelTankCapacity = fuelTankCapacity;
        CurrentFuelAmount = 0;
    }

    public virtual void Refuel(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Fuel must be a positive number");
            return;
        }

        if (CurrentFuelAmount + amount > FuelTankCapacity)
        {
            Console.WriteLine($"Cannot fit {amount} fuel in the tank");
            return;
        }

        CurrentFuelAmount += amount;
        Console.WriteLine($"Refueled {amount} liters. Current fuel: {CurrentFuelAmount} liters.");
    }

    public abstract void Drive(double distance);
}

class Car : Vehicle
{
    public Car(string brand, string model, double engineCapacity, double fuelConsumption, double fuelTankCapacity)
        : base(brand, model, engineCapacity, fuelConsumption, fuelTankCapacity) { }

    public override void Drive(double distance)
    {
        double fuelNeeded = distance * (FuelConsumption + 0.9) / 100;

        if (fuelNeeded > CurrentFuelAmount)
        {
            Console.WriteLine("Car needs refueling");
            return;
        }

        CurrentFuelAmount -= fuelNeeded;
        Console.WriteLine($"Car travelled {distance} km. Remaining fuel: {CurrentFuelAmount} liters.");
    }
}

class Truck : Vehicle
{
    public double LoadCapacity { get; private set; }
    public double CurrentLoad { get; private set; }

    public Truck(string brand, string model, double engineCapacity, double fuelConsumption, double fuelTankCapacity, double loadCapacity)
        : base(brand, model, engineCapacity, fuelConsumption, fuelTankCapacity)
    {
        LoadCapacity = loadCapacity;
        CurrentLoad = 0;
    }

    public void Load(double amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Cannot load a negative amount.");
            return;
        }

        if (CurrentLoad + amount > LoadCapacity)
        {
            Console.WriteLine($"Cannot load beyond capacity of {LoadCapacity} kg.");
            return;
        }

        CurrentLoad += amount;
        Console.WriteLine($"Loaded {amount} kg. Current load: {CurrentLoad} kg.");
    }

    public override void Drive(double distance)
    {
        double fuelNeeded = distance * (FuelConsumption + 1.6) / 100;

        if (fuelNeeded > CurrentFuelAmount)
        {
            Console.WriteLine("Truck needs refueling");
            return;
        }

        CurrentFuelAmount -= fuelNeeded;
        Console.WriteLine($"Truck travelled {distance} km. Remaining fuel: {CurrentFuelAmount} liters.");
    }
}

class Bus : Vehicle
{
    public bool HasPeople { get; private set; }

    public Bus(string brand, string model, double engineCapacity, double fuelConsumption, double fuelTankCapacity) 
        : base(brand, model, engineCapacity, fuelConsumption, fuelTankCapacity)
    {
        HasPeople = false;
    }

    public void SetPassengers(bool hasPeople)
    {
        HasPeople = hasPeople;
    }

    public override void Drive(double distance)
    {
        double additionalConsumption = HasPeople ? 1.4 : 0;
        double fuelNeeded = distance * (FuelConsumption + additionalConsumption) / 100;

        if (fuelNeeded > CurrentFuelAmount)
        {
            Console.WriteLine("Bus needs refueling");
            return;
        }

        CurrentFuelAmount -= fuelNeeded;
        Console.WriteLine($"Bus travelled {distance} km. Remaining fuel: {CurrentFuelAmount} liters.");
    }
}

// Завдання 3
abstract class Animal
{
    public string Name { get; protected set; }
    public double Weight { get; protected set; }
    public int FoodEaten { get; protected set; }

    protected Animal(string name, double weight)
    {
        Name = name;
        Weight = weight;
        FoodEaten = 0;
    }

    public abstract void AskForFood();
    public abstract void Eat(Food food);
}

abstract class Food
{
    public int Quantity { get; protected set; }
    protected Food(int quantity)
    {
        Quantity = quantity;
    }
}

class Vegetable : Food
{
    public Vegetable(int quantity) : base(quantity) { }
}

class Fruit : Food
{
    public Fruit(int quantity) : base(quantity) { }
}

class Meat : Food
{
    public Meat(int quantity) : base(quantity) { }
}

class Bird : Animal
{
    public double WingSize { get; private set; }

    public Bird(string name, double weight, double wingSize) : base(name, weight)
    {
        WingSize = wingSize;
    }

    public override void AskForFood()
    {
        Console.WriteLine("Hoot Hoot");
    }

    public override void Eat(Food food)
    {
        if (food is Meat)
        {
            Weight += 0.25 * food.Quantity;
            FoodEaten += food.Quantity;
        }
        else
        {
            Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }

    public override string ToString() => $"Bird [{Name}, {WingSize}, {Weight}, {FoodEaten}]";
}

class Mammal : Animal
{
    public string LivingRegion { get; private set; }

    protected Mammal(string name, double weight, string livingRegion) : base(name, weight)
    {
        LivingRegion = livingRegion;
    }
}

class Cat : Mammal
{
    public string Breed { get; private set; }

    public Cat(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion)
    {
        Breed = breed;
    }

    public override void AskForFood()
    {
        Console.WriteLine("Meow");
    }

    public override void Eat(Food food)
    {
        if (food is Vegetable || food is Meat)
        {
            Weight += food is Meat ? 0.3 * food.Quantity : 0.1 * food.Quantity;
            FoodEaten += food.Quantity;
        }
        else
        {
            Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }

    public override string ToString() => $"Cat [{Name}, {Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
}

class Dog : Mammal
{
    public Dog(string name, double weight, string livingRegion) 
        : base(name, weight, livingRegion) { }

    public override void AskForFood()
    {
        Console.WriteLine("Woof!");
    }

    public override void Eat(Food food)
    {
        if (food is Meat)
        {
            Weight += 0.4 * food.Quantity;
            FoodEaten += food.Quantity;
        }
        else
        {
            Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }

    public override string ToString() => $"Dog [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
}

class Tiger : Mammal
{
    public string Breed { get; private set; }

    public Tiger(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion)
    {
        Breed = breed;
    }

    public override void AskForFood()
    {
        Console.WriteLine("ROAR!!!");
    }

    public override void Eat(Food food)
    {
        if (food is Meat)
        {
            Weight += 1.0 * food.Quantity;
            FoodEaten += food.Quantity;
        }
        else
        {
            Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }

    public override string ToString() => $"Tiger [{Name}, {Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
}

class Mouse : Mammal
{
    public Mouse(string name, double weight, string livingRegion) 
        : base(name, weight, livingRegion) { }

    public override void AskForFood()
    {
        Console.WriteLine("Squeak");
    }

    public override void Eat(Food food)
    {
        if (food is Vegetable || food is Fruit)
        {
            Weight += 0.1 * food.Quantity;
            FoodEaten += food.Quantity;
        }
        else
        {
            Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }

    public override string ToString() => $"Mouse [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
}

// Основний клас
public class Program
{
    public static void Main(string[] args)
    {
        // Завдання 1-4
        Console.WriteLine("Транспортні засоби:");
        Vehicle car = new Car("Toyota", "Corolla", 1.8, 6.5, 50);
        car.Refuel(30);
        car.Drive(100);

        Truck truck = new Truck("Volvo", "FH", 13.0, 30, 300, 10000);
        truck.Refuel(100);
        truck.Load(5000);
        truck.Drive(150);

        Bus bus = new Bus("Mercedes", "Sprinter", 2.2, 12, 75);
        bus.Refuel(60);
        bus.SetPassengers(true);
        bus.Drive(200);
        
        // Завдання 3: Ферма
        Console.WriteLine("\nФерма:");
        Animal cat = new Cat("Whiskers", 4.5, "House", "Persian");
        Animal dog = new Dog("Rex", 12.0, "Yard");
        Animal tiger = new Tiger("Sheru", 180.0, "Forest", "Bengal");
        Animal mouse = new Mouse("Jerry", 0.2, "House");
        Animal bird = new Bird("Tweety", 0.1, 20);

        Food meat = new Meat(5);
        Food vegetable = new Vegetable(3);
        Food fruit = new Fruit(2);

        cat.AskForFood();
        cat.Eat(meat);
        Console.WriteLine(cat);

        dog.AskForFood();
        dog.Eat(meat);
        Console.WriteLine(dog);

        tiger.AskForFood();
        tiger.Eat(meat);
        Console.WriteLine(tiger);

        mouse.AskForFood();
        mouse.Eat(vegetable);
        Console.WriteLine(mouse);

        bird.AskForFood();
        bird.Eat(meat);
        Console.WriteLine(bird);
    }
}
