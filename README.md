# Large Language Model Enhanced Video Game

In this project, we created a Unity video game that utilizes LLMs to generate dialogue & combat flavor. This was both a great exercise in learning the mechanics of creating a video game from the ground up in Unity using C#, and with showcasing how LLMs can be integrated in more subtle ways to improve artistic experiences. 


The video game takes about 20 minutes to complete. It has:

--> 3 3D world maps, that the player can navigate through using their keyboard

--> 7 total NPCs, with AI-driven dialogue that's crafted to fit their character and progress the story

--> 3 combat encounters, Pokemon-style turn-based 3v1. Each of the three characters the player controls has 4 unique abilities that require management of their stamina, health, and armor.

--> 37 animations & 16 visual effects integrated into the various combat interactions 


In addition, we also experimented with additional features that utilize LLMs in more advanced ways. This includes a 'RAG' feature, where an LLM fetches information from a larger document that contains all the information about the world. We also made a system where an LLM could judge how well a character does in a task -- such as lying to a guard -- and then, based on the LLM's evaluation, decide which direction the story would take: whether they have to do an additional combat encounter, get an extra item, or get cursed with a negative debuff. Finally, we also experimented with a feature where a character could describe their own attack, and then an LLM would evaluate it to decide its damage and effects.

You can find more experimental scripts / draft work on this project here: https://github.com/ogreowl/capstone, or learn more about it here: https://bobbybecker2001.com/large-language-model-enhanced-video-game/


