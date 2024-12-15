using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator : Singleton<RandomNameGenerator>
{
    string[] adjectives = new string[] {"Happy", "Sad", "Angry", "Excited", "Nervous", "Tired", "Sleepy", "Hungry", "Full", "Thirsty",
    "Beautiful", "Ugly", "Pretty", "Handsome", "Smart", "Dumb", "Wise", "Strong", "Weak", "Tall",
    "Short", "Big", "Small", "Large", "Tiny", "Huge", "Fast", "Slow", "Quick", "Lazy",
    "Energetic", "Shy", "Brave", "Cowardly", "Kind", "Mean", "Friendly", "Unfriendly", "Helpful", "Selfish",
    "Polite", "Rude", "Generous", "Stingy", "Funny", "Serious", "Calm", "Loud", "Quiet", "Noisy",
    "Clean", "Dirty", "Messy", "Organized", "Disorganized", "Rich", "Poor", "Wealthy", "Broke", "Healthy",
    "Sick", "Fit", "Unfit", "Young", "Old", "New", "Ancient", "Modern", "Ancient", "Simple",
    "Complex", "Easy", "Difficult", "Hard", "Soft", "Hot", "Cold", "Warm", "Cool", "Spicy",
    "Sweet", "Salty", "Bitter", "Sour", "Fresh", "Rotten", "Delicious", "Disgusting", "Strong", "Weak",
    "Beautiful", "Ugly", "Colorful", "Boring", "Bright", "Dark", "Light", "Heavy", "Light", "Heavy",
    "Cheap", "Expensive", "Affordable", "Luxurious", "Friendly", "Dangerous", "Safe", "Risky", "Secure", "Dangerous",
    "Honest", "Dishonest", "Creative", "Boring", "Exciting", "Predictable", "Unpredictable", "Serene", "Chaotic"};

    string[] nouns = new string[] {"Apple", "Car", "Dog", "Cat", "House", "Tree", "Computer", "Book", "Phone", "Bicycle",
    "Student", "Teacher", "School", "City", "Country", "Mountain", "River", "Lake", "Ocean", "Forest",
    "Animal", "Person", "Child", "Adult", "Friend", "Family", "Team", "Company", "Company", "Office",
    "Market", "Restaurant", "Shop", "Store", "Church", "Hospital", "Clinic", "University", "College",
    "Road", "Street", "Building", "Sky", "Cloud", "Sun", "Moon", "Star", "Planet", "Earth",
    "Bird", "Fish", "Horse", "Cow", "Sheep", "Tree", "Flower", "Grass", "Soil", "Stone",
    "Water", "Fire", "Air", "Wind", "Snow", "Rain", "Storm", "Cloud", "Thunder", "Lightning",
    "Clothing", "Hat", "Shirt", "Pants", "Shoes", "Jacket", "Gloves", "Scarf", "Watch", "Bracelet",
    "Ring", "Necklace", "Pen", "Pencil", "Paper", "Desk", "Chair", "Table", "Lamp", "Clock",
    "Television", "Radio", "Camera", "Screen", "Keyboard", "Mouse", "Monitor", "Headphones", "Microphone",
    "Guitar", "Piano", "Violin", "Drums", "Trumpet", "Flute", "Bag", "Wallet", "Backpack", "Suitcase",
    "Ticket", "Passport", "Photo", "Memory", "Idea", "Thought", "Dream", "Goal", "Plan", "Project"};


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string RandomName()
    {
        string randomName;

        int adjective = Random.Range(0, adjectives.Length -1); 
        int noun = Random.Range(0, nouns.Length - 1);

       randomName = adjectives[adjective] + nouns[noun];

        return randomName;
    }
}
