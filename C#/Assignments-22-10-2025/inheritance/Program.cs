// See https://aka.ms/new-console-template for more information
using inheritanceAssigment;

Animal dog = new Dog();
dog.Speak();
Animal cat = new Cat();
cat.Speak();
Animal animal = new Cat();
animal.Speak();

////////////////////////////
///
Vehicle vehicle = new Vehicle();
vehicle.ShowType();
Car car = new Car();
car.ShowType();
Vehicle v = new Car();
v.ShowType();