using System.Collections.Generic;
using System.Linq;

namespace GenericEventSystem {
    public class EventName {
        public class Resources {
            public static string Reserve() { return "Resources_Reserve"; }
            public static string Add() { return "Resources_Add"; }
            public static string Remove() { return "Resources_Remove"; }
            public static string ResolveReserved() { return "Resources_ResolveReserved"; }
            public static string ResolveDebt() { return "Resources_ResolveDebt"; }
            public static string NotEnough() { return "Resources_NotEnough"; }
            public static string Overflow() { return "Resources_Overflow"; }
            public static List<string> Get() { return new List<string> { Reserve(), Add(), Remove(), ResolveReserved(), ResolveDebt(), NotEnough(), Overflow() }; }
        }

        public class Score {
            public static string ScoreIncreased() { return "World_ScoreIncreased"; }
            public static string ComboIncreased() { return "World_ComboIncreased"; }
            public static string MaxComboReached() { return "World_MaxComboReached"; }
            public static string Tick() { return "Network_PlayerLeft"; }
            public static List<string> Get() { return new List<string> { ScoreIncreased(), ComboIncreased(), MaxComboReached(), Tick() }; }
        }
        public class World {
            public static string Tick() { return "World_Tick"; }
            public static string Instantite() { return "World_Instantite"; }
            public static List<string> Get() { return new List<string> { Tick(), Instantite() }; }
        }
        //this shows how message names can be nested for convenience into types
        public class Input {
            public class Menus {
                public static string ShowSettings() { return "Input_Menus_ShowSettings"; }
                public static string None() { return null; }
                public static List<string> Get() { return new List<string> { ShowSettings(), None() }; }
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
            return new List<string> {}.Concat(Resources.Get())
                .Concat(Editor.Get())
                .Concat(Input.Get())
                .Concat(AI.Get())
                .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        }
    }
}