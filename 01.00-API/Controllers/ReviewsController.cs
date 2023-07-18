using API.SignalRHub;
using APIExtension.ClaimsPrinciple;
using APIExtension.Const;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IRepoWrapper repos;
        private IMapper mapper;
        private IHubContext<MeetingHub> meetingHub;

        public ReviewsController(IRepoWrapper repos, IMapper mapper, IHubContext<MeetingHub> meetingHub)
        {
            this.repos = repos;
            this.mapper = mapper;
            this.meetingHub = meetingHub;
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Test}/{Finnished.False}]Get review info"
           //, Description = "RevieweeId là id của người xin review (người gửi request)"
        )]
        [Authorize(Roles=Actor.Student)]
        [HttpGet("Meeting/{meetingId}")]
        public async Task<IActionResult> GetReviewForMeeting(int meetingId)
        {
            string reviewee = HttpContext.User.GetUsername();
            var list = repos.Reviews.GetList()
                            .Where(e => e.MeetingId == meetingId)
                            .Include(e => e.Reviewee)
                            .Include(e => e.Details).ThenInclude(e => e.Reviewer);
            var mapped = list
                .ProjectTo<ReviewSignalrDTO>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.False}]review info"
            , Description = "RevieweeId là id của người xin review (người gửi request)"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            string reviewee = HttpContext.User.GetUsername();
            Review endReview = await repos.Reviews.GetList()
                .Include(e => e.Reviewee)
                .Include(r => r.Details).ThenInclude(d => d.Reviewer)
                .SingleOrDefaultAsync(e => e.Id == reviewId);
            ReviewSignalrDTO mapped = mapper.Map<ReviewSignalrDTO>(endReview);
            return Ok(mapped);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.True}] Start a review"
            , Description = "RevieweeId là id của người xin review (người gửi request)"
        )]
        [Authorize(Roles=Actor.Student)]
        [HttpGet("Start")]
        public async Task<IActionResult> StartReviewForUserInMeeting(int meetingId)
        {
            int revieweeId = HttpContext.User.GetUserId();
            Review newReview = new Review
            {
                MeetingId = meetingId,
                RevieweeId = revieweeId
            };
            await repos.Reviews.CreateAsync(newReview);
            ReviewSignalrDTO mapped = mapper.Map<ReviewSignalrDTO>(newReview);
            await meetingHub.Clients.Group(meetingId.ToString()).SendAsync(MeetingHub.OnStartVoteMsg, mapped);
            return Ok(mapped);
        }

        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.True}] End a review"
          , Description = "RevieweeId là id của người xin review (người gửi request)"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("End")]
        public async Task<IActionResult> EndVote(int reviewId)
        {
            string reviewee = HttpContext.User.GetUsername();
            Review endReview = await repos.Reviews.GetList()
                .Include(e => e.Reviewee)
                .Include(r => r.Details).ThenInclude(d => d.Reviewer)
                .SingleOrDefaultAsync(e => e.Id == reviewId);
            ReviewSignalrDTO mapped = mapper.Map<ReviewSignalrDTO>(endReview);
            await meetingHub.Clients.Group(endReview.MeetingId.ToString()).SendAsync(MeetingHub.OnEndVoteMsg, mapped);
            return Ok(mapped);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.False}] Vote for a student review"
        //,Description = "Login for student with username or email. Return JWT Token if successfull"
        )]
        [Authorize(Roles=Actor.Student)]
        [HttpPost("Vote")]
        public async Task<IActionResult> VoteForReview(ReviewDetailSignalrCreateDto dto)
        {
            int reviewerId = HttpContext.User.GetUserId();
            ReviewDetail newReviewDetail = new ReviewDetail
            {
                ReviewId = dto.ReviewId,
                Comment = dto.Comment,
                Result = dto.Result,
                ReviewerId = reviewerId,
            };
            await repos.ReviewDetails.CreateAsync(newReviewDetail);
            ReviewDetailSignalrGetDto mappedDetail = mapper.Map<ReviewDetailSignalrGetDto>(newReviewDetail);

            Review changeReview = await repos.Reviews.GetList()
               .Include(e => e.Reviewee)
               .Include(r => r.Details).ThenInclude(d => d.Reviewer)
               .SingleOrDefaultAsync(e => e.Id == dto.ReviewId);
            ReviewSignalrDTO mapped = mapper.Map<ReviewSignalrDTO>(changeReview);
            return Ok(new { newDetail = mappedDetail, changeReview = mapped });
        }
    }
}
