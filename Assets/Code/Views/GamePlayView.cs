using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Code.ScriptableObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Code.Views
{
    public class GamePlayView : MonoBehaviour
    {
        public Action<PlayerInput, PlayerView> GamePlayStart { get; set; }
        public Action GamePlayFinish { get; set; }
        
        [SerializeField] private Transform player;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private float startPositionHorizontal = -6.5f;
        [SerializeField] private float startPositionVertical = 0f;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject chunkContainer;
        [Range(0.0f, 10f), SerializeField] private float moveHorizontalValue;
        private float _newPlayerPositionXAxis;
        private float _newCameraPositionXAxis;
        private List<GameObject> _chunks;
        private int _previousChunk;

        public void Initialize()
        {
            CreateChunks();
            SetUp();
            GamePlayStart(playerInput, playerView);
        }

        public void Finish()
        {
            gameObject.SetActive(false);
            ClearChunks();
            mainCamera.transform.position = new Vector3(0, 0, -10);
            GamePlayFinish();
        }

        private void ClearChunks()
        {
            _chunks.ForEach(Destroy);
        }

        private void Update()
        {
            var position = player.position;
            var cameraPosition = mainCamera.transform.position;
            var moveAmount = moveHorizontalValue * Time.deltaTime;
            _newPlayerPositionXAxis = position.x + moveAmount;
            _newCameraPositionXAxis = cameraPosition.x + moveAmount;
            if (HasPlayerCollided(position)) 
                Finish();
        }

        private bool HasPlayerCollided(Vector3 position) => 
            HasCollidedWithEndOfCamera(position.x, position.x+0.5f, _newCameraPositionXAxis - 17.5f * 0.5f);

        private void FixedUpdate()
        {
            player.position = new Vector2(_newPlayerPositionXAxis, player.position.y);
            MoveCamera();
            if (HasCameraGotANewChunk()) return;
            UpdateGameplayFor(chunk: _chunks[_previousChunk]);
            _previousChunk = CurrentCameraChunk().GetComponent<ChunkView>().Id;
        }

        private bool HasCameraGotANewChunk() =>
            _previousChunk == CurrentCameraChunk().GetComponent<ChunkView>().Id;

        private void MoveCamera()
        {
            var mainCameraTransform = mainCamera.transform;
            var cameraPosition = mainCameraTransform.position;
            mainCameraTransform.position = new Vector3(_newCameraPositionXAxis, cameraPosition.y, cameraPosition.z);
        }

        private GameObject CurrentCameraChunk() => 
            _chunks.First(HasCollided);

        private bool HasCollided(GameObject chunk)
        {
            var chunkPosition = chunk.transform.position;
            var chunkMinX = chunkPosition.x - levelGenerator.Width * 0.5f;
            var chunkMaxX = chunkPosition.x + levelGenerator.Width * 0.5f;
            var cameraMinX = _newCameraPositionXAxis - 17.5f * 0.5f;
            return HasCollidedWithEndOfCamera(chunkMinX, chunkMaxX, cameraMinX);
        }

        private bool HasCollidedWithEndOfCamera(float colliderMinX, float colliderMaxX, float cameraMinX) =>
            colliderMinX <
            cameraMinX &&
            colliderMaxX >=
            cameraMinX;

        private void SetUp()
        {
            _newPlayerPositionXAxis = player.position.x;
            _newCameraPositionXAxis = mainCamera.transform.position.x;
            gameObject.SetActive(true);
            _previousChunk = 0;
        }

        private void CreateChunks()
        {
            _chunks = Enumerable.Range(0, levelGenerator.AmountOfChunks)
                .Select(_ => InitializeChunkContainersWithFirstChunks()).ToList();
            for (var i = 0; i < _chunks.Count; i++)
            {
                var chunk = _chunks[i];
                chunk.transform.position =
                    new Vector3(startPositionHorizontal + (i * levelGenerator.Width), startPositionVertical);
                chunk.GetComponent<ChunkView>().Id = i;
            }
        }

        private GameObject InitializeChunkContainersWithFirstChunks()
        {
            var go = Instantiate(chunkContainer, transform);
            AddChunkToContainer(go);
            return go;
        }

        private void AddChunkToContainer(GameObject container)
        {
            Instantiate(levelGenerator.GetChunk(), container.transform);
        }

        private void UpdateGameplayFor(GameObject chunk)
        {
            chunk.transform.position =
                new Vector3(chunk.transform.position.x + levelGenerator.TotalWidth,
                    startPositionVertical);
            Destroy(chunk.transform.GetChild(0).gameObject);
            AddChunkToContainer(chunk);
        }
    }
}