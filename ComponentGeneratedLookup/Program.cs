using System;
using System.Collections.Generic;

namespace BestGame {

    public class Program {
        public static void Main(string[] args) {

            var health = new HealthComponent {value = 10};
            var name = new NameComponent {value = "John"};

            Console.WriteLine($"{name.value} has {health.value} health.");

            Console.WriteLine($"HealthComponent is at index {GetIndexForType(health)}");
            Console.WriteLine($"The component type at index 1 is {GetTypeFromIndex(1).Name}");


        }

        public static int GetIndexForType<T>(Component<T> component) {
            string typeDetails = component.GetHash();
            return Generated.TypeHashToIndex[typeDetails];
        }
        
        /// <summary>
        /// This reflection is expensive and generally should not be used.
        /// Ok to use for debugging.
        /// </summary>
        public static Type GetTypeFromIndex(int index) {
            string typeDetails = Generated.ComponentsTypes[index];
            return Type.GetType(typeDetails);
        }

    }

    public static class Generated {
        public static readonly string[] ComponentsTypes = {
            "BestGame.HealthComponent, ComponentGeneratedLookup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "BestGame.NameComponent, ComponentGeneratedLookup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        public static readonly Dictionary<string, int> TypeHashToIndex = new Dictionary<string, int> {
            { "BestGame.HealthComponent, ComponentGeneratedLookup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", 0 },
            { "BestGame.NameComponent, ComponentGeneratedLookup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", 1 } 
        };
    }

    public class HealthComponent : Component<int> { }

    public class NameComponent : Component<string> { }

    public class Component<T> {
        public T value;

        public string GetHash() {
            return GetType().AssemblyQualifiedName;
        }
    }
}
