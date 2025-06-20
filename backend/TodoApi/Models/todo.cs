using System;
using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace TodoApi.Models
{
    [FirestoreData]
    public class Todo
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }

        [Required]
        [FirestoreProperty]
        public string? Title { get; set; }

        [FirestoreProperty]
        public Priorities Priority { get; set; }

        [FirestoreProperty]
        public bool IsComplete { get; set; }

        [FirestoreProperty]
        public DateTime? CreatedAt { get; set; }

        [FirestoreProperty]
        public DateTime? UpdatedAt { get; set; }
    }

    public enum Priorities
    {
        Low,
        Medium,
        High
    }
}