﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Loader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: JsonLoader <path-to-json-file>");
                return;
            }

            var filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' not found.");
                return;
            }

            try
            {
                var jsonContent = File.ReadAllText(filePath);
                var rootObject = JsonSerializer.Deserialize<CardList>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                Console.WriteLine("JSON file loaded successfully!");
                Console.WriteLine("Displaying data:");
                Console.WriteLine(JsonSerializer.Serialize(rootObject, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Represents the root JSON object.
    /// </summary>
    public class CardList
    {
        /// <summary>
        /// Name of the card list.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Version string of the card list.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Unique identifier for the card list.
        /// </summary>
        [JsonPropertyName("uniqueId")]
        public string UniqueId { get; set; }

        /// <summary>
        /// Optional attributes (attribute-note pairs) that apply to the card list.
        /// </summary>
        [JsonPropertyName("attributes")]
        public List<AttributeItem> Attributes { get; set; }

        /// <summary>
        /// Optional notes about the card list.
        /// </summary>
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; }

        /// <summary>
        /// List of card sets.
        /// </summary>
        [JsonPropertyName("sets")]
        public List<Set> Sets { get; set; }
    }

    /// <summary>
    /// Defines an attribute and its note.
    /// </summary>
    public class AttributeItem
    {
        /// <summary>
        /// The attribute.
        /// </summary>
        [JsonPropertyName("attribute")]
        public string Attribute { get; set; }

        /// <summary>
        /// Additional note about the attribute.
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; }
    }

    /// <summary>
    /// Defines a set of cards.
    /// </summary>
    public class Set
    {
        /// <summary>
        /// Unique identifier for the set.
        /// </summary>
        [JsonPropertyName("uniqueId")]
        public string UniqueId { get; set; }

        /// <summary>
        /// Name of the set.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional notes about the set.
        /// </summary>
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; }

        /// <summary>
        /// Optional number that the cards in this set are numbered to.
        /// </summary>
        [JsonPropertyName("numberedTo")]
        public int? NumberedTo { get; set; }

        /// <summary>
        /// Optional list of insert odds for the set.
        /// </summary>
        [JsonPropertyName("insertOdds")]
        public List<InsertOdd> InsertOdds { get; set; }

        /// <summary>
        /// Variations that apply to the entire set.
        /// </summary>
        [JsonPropertyName("variations")]
        public List<Variation> Variations { get; set; }

        /// <summary>
        /// Parallel versions of the set.
        /// </summary>
        [JsonPropertyName("parallels")]
        public List<Parallel> Parallels { get; set; }

        /// <summary>
        /// List of cards in this set.
        /// </summary>
        [JsonPropertyName("cards")]
        public List<Card> Cards { get; set; }
    }

    /// <summary>
    /// Represents a variation, applicable either to a set or an individual card.
    /// </summary>
    public class Variation
    {
        /// <summary>
        /// Name/title of the variation.
        /// </summary>
        [JsonPropertyName("variation")]
        public string VariationName { get; set; }

        /// <summary>
        /// Optional note about the variation.
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; }

        /// <summary>
        /// Optional list of insert odds specific to this variation.
        /// </summary>
        [JsonPropertyName("insertOdds")]
        public List<InsertOdd> InsertOdds { get; set; }

        /// <summary>
        /// Optional parallels that apply to this variation.
        /// </summary>
        [JsonPropertyName("parallels")]
        public List<Parallel> Parallels { get; set; }

        /// <summary>
        /// Optional list of attributes for the variation.
        /// </summary>
        [JsonPropertyName("attributes")]
        public List<string> Attributes { get; set; }

        /// <summary>
        /// Optional number that the variation is numbered to.
        /// </summary>
        [JsonPropertyName("numberedTo")]
        public int? NumberedTo { get; set; }
    }

    /// <summary>
    /// Represents a parallel version of a set or card.
    /// </summary>
    public class Parallel
    {
        /// <summary>
        /// Name/title of the parallel.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional number indicating how many cards are in this parallel.
        /// </summary>
        [JsonPropertyName("numberedTo")]
        public int? NumberedTo { get; set; }

        /// <summary>
        /// Optional notes about the parallel.
        /// </summary>
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; }

        /// <summary>
        /// Optional list of insert odds for the parallel.
        /// </summary>
        [JsonPropertyName("insertOdds")]
        public List<InsertOdd> InsertOdds { get; set; }
    }

    /// <summary>
    /// Represents insert odds information.
    /// </summary>
    public class InsertOdd
    {
        /// <summary>
        /// The product associated with the insert.
        /// </summary>
        [JsonPropertyName("product")]
        public string Product { get; set; }

        /// <summary>
        /// The odds value, formatted as "number:number,number,..."
        /// </summary>
        [JsonPropertyName("odds")]
        public string Odds { get; set; }
    }

    /// <summary>
    /// Represents an individual card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Unique identifier for the card.
        /// </summary>
        [JsonPropertyName("uniqueId")]
        public string UniqueId { get; set; }

        /// <summary>
        /// Optional card number.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Name of the card.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional list of attributes for the card.
        /// </summary>
        [JsonPropertyName("attributes")]
        public List<string> Attributes { get; set; }

        /// <summary>
        /// Optional list of insert odds for the card.
        /// </summary>
        [JsonPropertyName("insertOdds")]
        public List<InsertOdd> InsertOdds { get; set; }

        /// <summary>
        /// Optional note about the card.
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; }

        /// <summary>
        /// Variations specific to this card.
        /// </summary>
        [JsonPropertyName("variations")]
        public List<Variation> Variations { get; set; }

        /// <summary>
        /// Parallel versions of this card.
        /// </summary>
        [JsonPropertyName("parallels")]
        public List<Parallel> Parallels { get; set; }
    }
}
