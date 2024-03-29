﻿using System.Collections.Generic;
using System.Linq;

namespace GenericEventSystem {
    public class EventName {
        public class Beats {
            public static string BeatHitInput() { return "Resources_BeatHit"; }
            public static string BeatHitResult() { return "Resources_BeatHitResult"; }
            public static string BeatCreated() { return "Resources_BeatCreated"; }
            public static string StartBeatTrack() { return "Resources_StartBeatTrack"; }
            public static List<string> Get() { return new List<string> { BeatHitInput(), BeatHitResult(), BeatCreated(), StartBeatTrack() }; }
        }
        public class Item
        {
            // Trigger when using input to dodge reverse beats
            public static string DodgeInput() { return "Item_DodgeInput"; }

            // Trigger to throw item, contains type

            public static string Throw() { return "Item_Throw"; }

            // Triggered when the item reaches destination
            public static string CheckHit() { return "Item_CheckHit"; }

            // Triggered when the item hits the commedian, contains type
            public static string Hit() { return "Item_Hit"; }

            // Triggered when the item was dodged, contains type
            public static string Dodge() { return "Item_Dodge"; }
            public static List<string> Get() { return new List<string> { DodgeInput(), Throw(), CheckHit(), Hit(), Dodge() }; }
        }
        public class Score {
            public static string ScoreIncreased() { return "World_ScoreIncreased"; }
            public static string ComboIncreased() { return "World_ComboIncreased"; }
            public static string MaxComboReached() { return "World_MaxComboReached"; }
            public static string Tick() { return "Network_PlayerLeft"; }
            public static List<string> Get() { return new List<string> { ScoreIncreased(), ComboIncreased(), MaxComboReached(), Tick() }; }
        }
        public class World {
            public static string GameStateChange() { return "World_GameStateChanged"; }
            public static string Instantite() { return "World_Instantite"; }
            public static string CrowdStateChange() {return "World_CrowdStateChanged"; }
            public static List<string> Get() { return new List<string> { GameStateChange(), Instantite(), CrowdStateChange() }; }
        }
        //this shows how message names can be nested for convenience into types
        public class Input {
            public class Menus {
                public static string ShowSettings() { return "Input_Menus_ShowSettings"; }
                public static string SelectCharacterNext() { return "Input_Menus_SelectCharacterNext"; }
                public static string SelectCharacterPrevious() { return "Input_Menus_SelectCharacterPrevious"; }
                public static string None() { return null; }
                public static List<string> Get() { return new List<string> { ShowSettings(), SelectCharacterNext(), SelectCharacterPrevious(), None() }; }
            }
            public static string PlayersReady() { return "Input_PlayersReady"; }
            //nesting can be done indefinitely but Get() function must get it's depth as well as follows:
            public static List<string> Get() {
                return new List<string> {
                        PlayersReady(),
                    }.Concat(Menus.Get())
                    .Concat(Score.Get())
                    .Concat(World.Get())
                    .ToList();
            }
        }
        //Some examples what other classes could be used to better arrange messaging into
        public class Editor {
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }
        }
        public class AI {
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }
        }
        //This master Get() function returns all of the messages, thus enabling things like Editor extensions, i.e. the list picker/selector.
        public static List<string> Get() {
            return new List<string> {}.Concat(Beats.Get())
                .Concat(Item.Get())
                .Concat(Editor.Get())
                .Concat(Input.Get())
                .Concat(AI.Get())
                .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        }
    }
}