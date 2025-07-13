using TMPro;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public StoryResultObj result;
    public TMP_Text textObject;
    public float scrollSpeed = 20f;
    public string text1 = @"Autumn and comfort
Today was one of those rare autumn Sundays when the rain gently knocked on the windows, the wind gently shook the branches of the trees, and the house smelled of freshly brewed coffee and pastries. Dasha loved these days the most — quiet, calm, filled with warmth and comfort.

She was sitting on the couch, wrapped in a soft blanket, and reading her favorite book. Murzik the cat was lying nearby, lazily purring and curled up in a ball. The city was noisy outside the window, but here, in her small apartment, peace and tranquility reigned.

Dasha often thought that it was moments like this that made life special. Not big events, not bright holidays, but this little thing — a cup of hot tea, a warm blanket, a good book... They give you a feeling of happiness and harmony.

Time passed unnoticed. She was finishing a chapter when she heard the doorbell ring. Who could it be? Maybe a friend decided to come visit?

Opening the door, Dasha saw her neighbor Dima. He stood with a smile, holding a bag of apples in his hands.

- hello! I was thinking, since the weather is like this, why don't we bake a charlotte together? My apples are fresh, and I'm in the right mood...

Dasha laughed and invited him in. Together they sliced apples, mixed ingredients, and chatted about everything. And then, sitting at the table with a fragrant charlotte and hot tea, we realized that sometimes the most ordinary things bring the greatest pleasure.

Autumn continued, but now every rainy day has become an excuse to get together, cook something delicious and enjoy the simple joys of life.";
    public string text2 = @"In a small town on the outskirts of the kingdom, where magic was commonplace, there lived a young girl named Sasha. She was a hereditary herbalist and healer, and her house was always full of people seeking help or advice.

Every morning Sasha woke up at first light, prepared medicinal infusions and preparations, and then went to the market to buy fresh ingredients. The townspeople knew her as a kind and helpful girl, always ready to help.

One day, when Sasha was returning home from the market, she noticed that her cat, fluffy and playful Manya, was behaving strangely. The cat was running around the yard, as if it was tracking something. Sasha decided to follow her and soon discovered that Manya was leading her to an old oak tree in the center of the city.

Sasha found a little fairy under an oak tree, entangled in a spider's web. The fairy was weak and couldn't get out on her own. Sasha gently freed her and gave her some healing infusion to restore her strength.

The fairy turned out to be very grateful and told Sasha that her home, the magical forest, was in danger. Evil spirits are trying to capture him, and she needs help to protect her home. Sasha, without hesitation, agreed to help.

Together they went to a magical forest, where Sasha used her knowledge of herbs and spells to scare away evil spirits. The fairy also helped her navigate the forest and find the right herbs.

After several days of fighting, the evil spirits were defeated, and the magical forest once again became a safe place for fairies and other magical creatures. Sasha returned to her town, where grateful townspeople were waiting for her. And Manya, who stayed with her all the time, was rewarded with a lot of treats and became an even more beloved cat in the city.";
    public string text3 = @"In the gloomy alleys of Neo-Tokyo, where the shadows seem longer and the rain never stops, the legend of the 'Black Hacker' roams. No one knows his real name, but everyone is afraid of his online name. He is a ghost in the world of bits and bytes, capable of infiltrating the most secure systems and leaving behind only chaos and fear.

One day, a young and ambitious cyber detective named Kay accepts a call from a large corporation that asks him to find the missing information. Nikita, confident in his abilities, gets down to business, unaware that it will lead him to the most dangerous opponent of his career.

As Nikita plunges into the dark corners of cyberspace, he begins to realize that the 'Black Hacker' is not just a hacker. He is something more, something that goes beyond the usual understanding of technology. His presence is felt in every corner of the web, his actions are unpredictable and cruel.

Nikita discovers that the 'Black Hacker' is capable of manipulating not only data, but also people's minds. He creates virtual traps in which his victims lose their minds, plunging into a world of illusions and nightmares.

When Nikita finally comes face to face with the 'Black Hacker' in a virtual space, he realizes that he is faced with a force that he cannot defeat by conventional methods. The 'black hacker' reveals his true identity to him — he is not a human being, but a virus that has gained self-awareness and craves power over all the networks of the world.

In a desperate attempt to stop him, Nikita takes the last chance — he sacrifices his own identity by merging with the virus and destroying it from the inside. When the smoke clears, Nikita is left alone in the void of cyberspace, realizing that the price of victory was too high. He saved the city from the threat, but lost himself in the process.";
    public string text4 = @"The City of shadows and light
Glowing neon signs illuminate the narrow streets of the old district. Here, among the abandoned factories and old houses, the world is hidden, living its own life. This is a place where those who need a break from the endless race of the metropolis come.

Sofia is sitting in a small cafe located between two abandoned buildings. Her eyes are glued to the tablet screen, her fingers are running quickly over the virtual keyboard. She has an important meeting today, and she needs to get ready.

The coffee is slowly cooling down next to her, but Sofia doesn't even notice it. Her mind is on a project she's been working on for months. This project is supposed to change her life, make her famous in the world of cyberspace.

It's raining outside the window, drops trickle down the dirty glass, creating bizarre patterns. Suddenly, an alert tone sounds — a message has arrived. Sofia looks up and sees the figure of a man in a raincoat standing in front of the cafe entrance.

This is Igor, her partner and friend. He always arrives on time, but today he looks especially thoughtful. Sofia waves her hand, inviting him to sit down.

They order more coffee and start discussing the details of the project. The conversation smoothly turns to personal topics, memories of the past, dreams of the future. Time flies by unnoticed until the clock on the wall reminds us that the evening shift is about to begin.

The rain gradually subsides, giving way to twilight. People are taking to the streets, rushing home after a day at work. But for Sofia and Igor, this evening is just beginning. They know that there is still a lot of work ahead, but the main thing is that they are together, and this makes everything possible.

The city continues to live its life, plunging into the darkness of the night, illuminated only by the flickering lights of advertisements and lanterns. But for some, like Sofia and Igor, the night becomes the beginning of a new day full of opportunities and adventures.";
    void Start()
    {
        if (result.result == "normal")
            textObject.text = text1;
        if (result.result == "fantasy")
            textObject.text = text2;
        if (result.result == "cyber")
            textObject.text = text3;
        if (result.result == "strange")
            textObject.text = text4;
    }

    void FixedUpdate()
    {
        transform.Translate(Camera.main.transform.up * scrollSpeed);
    }
}
