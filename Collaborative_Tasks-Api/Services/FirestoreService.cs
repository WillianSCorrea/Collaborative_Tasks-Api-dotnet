using Google.Cloud.Firestore;

namespace Collaborative_Tasks.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _db;

        public FirestoreService(IConfiguration config)
        {
            _db = FirestoreDb.Create(config["Firebase:ProjectId"]);
        }

        public async Task AddAsync<T>(string collection, string id, T data)
        {
            await _db.Collection(collection).Document(id).SetAsync(data);
        }

        public async Task<T?> GetAsync<T>(string collection, string id)
        {
            var snap = await _db.Collection(collection).Document(id).GetSnapshotAsync();
            return snap.Exists ? snap.ConvertTo<T>() : default;
        }

        public async Task UpdateAsync<T>(string collection, string id, T data)
        {
            await _db.Collection(collection).Document(id)
                .SetAsync(data, SetOptions.MergeAll);
        }

        public async Task DeleteAsync(string collection, string id)
        {
            await _db.Collection(collection).Document(id).DeleteAsync();
        }
    }
}