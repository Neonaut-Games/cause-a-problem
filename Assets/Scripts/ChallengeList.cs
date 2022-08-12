using System.Collections.Generic;
using Random = System.Random;

public class ChallengeList
{

    #region Global Storage

    private readonly Random _random = new();

    /* Tier 1
     Family friendly; play it with your grandma! */
    private readonly string[] _familyFriendly = {
        "What's your biggest pet peeve?",
        "Complement a person in the room.",
        "Pick one person in the room. What's your favorite thing about them?",
        "Pick one person in the room and make a 30 second skit together. Perform it for the group.",
        "What would you do if you could become invisible for a day?",
        "What's your biggest irrational fear?",
        "Have you ever stolen something? What was it?",
        "Imitate someone else in the room.",
        "Change your profile picture on any social media platform to someone else in the room.",
    };
    
    /* Tier 2
     Talks about relationships, kissing, politics, etc. */
    private readonly string[] _tier2 = {
        "Tell everyone about your first kiss.",
        "Let someone read your last text aloud.",
        "Let someone look through your DMs and read at least 2 aloud.",
        "Rate the person before you on a scale from 1-10.",
        "What's the most illegal thing you've done?",
        "When was the last time you lied & what was it?",
        "Tell me about the most recent physical altercation you've been in.",
        "Tell everyone your phone password; you can't change it for 24 hours.",
        "Pick one person in the room. What's your favorite thing about them?",
        "What's your hottest political take?",
        "Start a timer for 1 minute; let everyone in the room roast you without bounds.",
        "Would you kiss anyone in the room? You don't have to say who.",
        "Let the person before you pull up a random person from your following list and like one of their photos from 1+ years ago.",
        "If you had to date someone in the room who would it be?",
        "Take a selfie with someone's leg and post it to one of your stories on a social media platform with no context.",
    };
    
    /* Tier 3
     NSFW, Talks about sexuality, dares to kiss people, etc. */
    private readonly string[] _nsfw = {
        "When was the last time you masturbated?",
        "Have you ever seen gay porn?",
        "What's your weirdest kink?",
        "What's your rice purity score? Take the test if you haven't.",
        "Have you ever sent nudes & to who?",
        "Show one person your My Eyes Only for 5 seconds or greater.",
        "If you had to fuck one person in the room, who would it be?",
        "What's the worst thing you've done that isn't illegal?",
        "How much money would it take for you to sell feet pics?",
        "Do you have any anonymous social media accounts & what are they used for?",
        "What's one thing you'd do if there were no consequences?",
        "What's the worst thing you've ever said to someone?",
        "Kiss the person after you.",
        "Have you ever subscribed to anyone on OnlyFans?",
        "Say the number of people in the room you'd be willing to kiss.",
        "Describe the weirdest dream you've ever had.",
        "Have you ever asked anyone for nudes?",
        "Unzip the zipper of the person after you; if you laugh or break eye contact, restart.",
        "Fuck, Marry, Kill. Everyone in the room.",
        "Would you have sex with anyone in this room? You don't have to say who.",
        "Have you ever put anything up your butt?",
        "Have you ever thought about anyone in the room sexually?",
        "Make out with the hand of the person before you for 10 seconds.",
        "Who is the most attractive person in the room?",
        "Who's the last person you thought about sexually?",
        "What's the worst thought you've ever had about another person?",
        "Who in the room would you never let around your kids?",
        "Guess who in the room has the biggest dick (for girls, dick energy)?",
        "What's the most racist thing you've ever said?",
        "Run your fingers through the person to your left's hair for the next minute.",
        "You and the person before you roast each other for the next 20 seconds.",
    };
    
    /* Basically "Tier 99"
     Not even the FBI could get that sh*t outta me.. */
    private readonly string[] _terrible = {
        "Have you ever wanted to legitimately kill somebody?",
        "What's the youngest age you'd date? Obama is listening.",
        "What's the most racist thing you've ever said? Say it with passion.",
        "Your father or mother switches bodies with your girlfriend or boyfriend. The only way to switch them back is to have sex with either your partner in your parent's body, or vice versa. Which do you choose?",
        "Would you rather fuck a goat and nobody know, or not fuck a goat and everyone think you did.",
        "Imagine the person to your left's breast's are flight controls and pretend to land a spaceship for 10 seconds.",
        "Have up to 4 other players rub one of your limbs for the next 30 seconds.",
        "Have you ever had a crush on a teacher? Describe it.",
    };
    
    /* Deep Questions
     Things that make you think about life, etc. */
    private readonly string[] _deep = {
        "What would you do if you had 6 months to live?",
        "What would you do if you had a billion dollars?",
        "What advice would you give yourself 10 years ago?",
        "What do you hope will be the same 10 years from now?",
        "What do you hope will be different 10 years from now?",
        "What is your idea of perfect happiness?",
        "When and where were you happiest?",
        "Why do you get out of bed in the morning?",
        "What do you consider the lowest depth of misery?",
        "What is your most marked characteristic?",
        "What is your greatest fear?",
        "What is the trait you dislike the most in others?",
        "On what occasion do you lie?",
        "What is your greatest extravagance?",
        "What do you consider the most overrated virtue?",
        "If you could change one thing about yourself, what would it be?",
        "Which talent would you most like to have?",
        "What do people frequently misunderstand about you?",
        "What is the quality you most like in a significant other?",
        "What do you most value in your friends?",
        "What do you consider your greatest achievement?",
        "If you could give everyone in the world one gift, what would it be?",
        "What was your greatest waste of time?",
        "What do you find painful but worth doing?",
        "Where would you most like to live?",
        "What is your most treasured possession?",
        "Who is your best friend?",
        "Who or what is the greatest love of your life?",
        "Which living person do you most admire?",
        "Who is your hero of fiction?",
        "Which historical figure do you most identify with?",
        "What is your greatest regret?",
        "How do you want to die?",
        "What is your lifelong motto?",
        "What is the best compliment you ever received?",
        "What is the luckiest thing that happened to you?",
        "What in the world makes you feel the most hopeful?",
    };

    #endregion

    #region Game Sesssion Specifics

    private readonly int _tier;
    private readonly bool _isTerrible;
    private readonly bool _isDeep;
    private readonly List<string> _session = new();

    #endregion

    public ChallengeList(int tier, bool deep, bool nsfw)
    {
        _tier = tier;
        _isDeep = deep;
        _isTerrible = nsfw;
        
        _session.AddRange(_familyFriendly);
        if (tier >= 2) _session.AddRange(_tier2);
        if (tier >= 3) _session.AddRange(this._nsfw);
        
        if (deep) _session.AddRange(_deep);
        if (nsfw) _session.AddRange(_terrible);
    }
    
    public string Get()
    {
        if (_session.Count == 0) return null;
        
        var index = _random.Next(0, _session.Count);
        var challenge = _session[index];
        _session.RemoveAt(index);
        return challenge;
    }

    public override string ToString()
    {
        return "{Tier " + _tier + ", Deep? " + _isDeep + ", Terrible? " + _isTerrible + "}";
    }
}
