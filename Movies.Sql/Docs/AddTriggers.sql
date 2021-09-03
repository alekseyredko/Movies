create trigger ReviewerWatchHistory_Insert_Update
on ReviewerWatchHistory
after insert, update
as
update ReviewerWatchHistory
set LastWatchTime = GETUTCDATE()
go

alter trigger Review_Insert_Update_Delete
on Review
after insert, update, delete
as

declare @movieId int,
		@reviewId int

set @movieId = (select MovieId from inserted)

if isnull(@movieId, -1) = -1
	set @movieId = (select MovieId from deleted)
else
	begin
		set @reviewId = (select ReviewId from inserted)

		update Review 
		set LastUpdate = GETUTCDATE() 
		where Review.ReviewId = @reviewId
	end

update Movie
set Rate = (select sum(Rate) from Review) / (select count(*) from Review),
	LastUpdate = GETUTCDATE()
where Movie.MovieId = @movieId

