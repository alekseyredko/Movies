using System;
using System.Collections.Generic;

#nullable disable

namespace Movies.Domain.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieGenres = new HashSet<MovieGenre>();
            MoviesActors = new HashSet<MoviesActor>();
            Reviews = new HashSet<Review>();
            Genres = new HashSet<Genre>();
            Actors = new HashSet<Actor>();
            Reviewers = new HashSet<Reviewer>();
            ReviewerWatchHistories = new HashSet<ReviewerWatchHistory>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public TimeSpan Duration { get; set; }
        public double? Rate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? ProducerId { get; set; }

        public virtual Producer Producer { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Actor> Actors { get; set; } 
        public virtual ICollection<Reviewer> Reviewers { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        public virtual ICollection<MoviesActor> MoviesActors { get; set; }
        public virtual ICollection<ReviewerWatchHistory> ReviewerWatchHistories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
