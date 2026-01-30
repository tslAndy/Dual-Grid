# Dual Grid
Advanced version of dual grid. Main problem I've tried to solve is that standard version of dual grid can handle only two types of tiles. There were couple possible solutions to this problem: 
1. Create separate tilemap for each type of tile
2. Use mix of rule-tiles and dual-tiles

The problem is that these systems are complicated, also I wanted for tiles to be on same tilemap, for simplifying pathfinding and realtime tilemap editing.
The solution is to use base tilemap for all types of tiles, and 3 additional tilemaps. We define draw order for each type of tiles and algorithm automatically figures out which tiles should be on additional tilemaps, so we need to modify only base tilemap. For reducing amount of tiles, only borders between tiles are considered.

Base tilemap:
<img width="1095" height="618" alt="Screenshot_20260130_155627" src="https://github.com/user-attachments/assets/fdbcaf51-deb9-4f55-b5a2-4554c4318e94" />

Borders (counted by algorithm):
<img width="972" height="556" alt="Screenshot_20260130_155717" src="https://github.com/user-attachments/assets/37f0bfb1-8822-4f6b-9cbc-96417ad9cd52" />

Final result:
<img width="1092" height="612" alt="Screenshot_20260130_155644" src="https://github.com/user-attachments/assets/b8c72672-f868-45b5-9991-fc728a4e85f5" />
