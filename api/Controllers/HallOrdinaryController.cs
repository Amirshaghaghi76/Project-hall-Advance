namespace api.Controllers;
public class HallOrdinaryController(IHallRepository _hallRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInHallDto?>> Create(RegisterHallDto userInput, CancellationToken cancellationToken)
    {
        LoggedInHallDto? loggedInHallDto = await _hallRepository.CreateAsync(userInput, cancellationToken);

        if (loggedInHallDto is null)
            return BadRequest("this hall is taken.");

        return loggedInHallDto;
    }
    
    // [HttpGet("get-by-name/{name}")]

    // public ActionResult<Hall> Get(string name)
    // {
    //     Hall ordinaryHall = _collection.Find(ordinaryHall =>
    //     ordinaryHall.Name == name.ToLower()).FirstOrDefault();

    //     if (ordinaryHall == null)
    //     {
    //         return NotFound("this is no such name");
    //     }

    //     return ordinaryHall;
    // }

    // [HttpGet]
    // public ActionResult<List<Hall>> GetAll()
    // {
    //     List<Hall> ordinaryHall = _collection.Find<Hall>(new BsonDocument()).ToList();
    //     if (!ordinaryHall.Any())
    //     {
    //         return Ok("The list is empty.");
    //     }

    //     return ordinaryHall;
    // }

    // [HttpPut("update/{hallId}")]
    // public ActionResult<UpdateResult> UpdateHall(string hallId, Hall hallIn)
    // {
    //     var UpdatedHall = Builders<Hall>.Update
    //     .Set((Hall doc) => doc.Name, hallIn.Name.ToLower())
    //     .Set(doc => doc.City, hallIn.City)
    //     .Set(doc => doc.Capacity, hallIn.Capacity)
    //     .Set(doc => doc.PhoneNumber, hallIn.PhoneNumber)
    //     .Set(doc => doc.Parking, hallIn.Parking)
    //     .Set(doc => doc.WeddingRoom, hallIn.WeddingRoom)
    //     .Set(doc => doc.FreeWifi, hallIn.FreeWifi)
    //     .Set(doc => doc.Cofe, hallIn.Cofe);

    //     return _collection.UpdateOne<Hall>(doc => doc.Id == hallId, UpdatedHall);
    // }

    // [HttpDelete("delete/{hallId}")]
    // public ActionResult<DeleteResult> Delete(string hallId)
    // {
    //     return _collection.DeleteOne<Hall>(doc => doc.Id == hallId);

}

