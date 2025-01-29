class Human {
    public Human() {
        Console.WriteLine("Human Constructor");
    }
    public string? Name {get;set;}
    public int Age {get;set;}
    public virtual void SayHello() {
        Console.WriteLine("Hello, " + Name);
    }
}
class Person : Human {
    public Person() {
        Console.WriteLine("Person Constructor");
    }
    public override void SayHello() {
        Console.WriteLine("Say Hello : " + Name);
    }
}