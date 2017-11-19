using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class Image {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
    }
}
