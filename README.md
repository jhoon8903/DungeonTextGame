
Dungeon RPG
--

### 프로젝트 구성

|목록|내용|
|:--|:--|
|타이틀|Dungeon RPG|
|기간|23.11.09 ~ 23.11.11|
|개발 인원|1 명 |
|IDE|Rider|
|Devlop Env|Mac M1 Silcon|
|Language|C#|
|Redering|Rider Console|

### Class 구성

![](https://mermaid.ink/svg/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5UaXRsZSAtLXw-IEFjY291bnRcbkNoYXJhY3RlciAtLXw-IEFjY291bnRcbkl0ZW0gLS18PiBBY2NvdW50XG5BY2NvdW50IC0tfD4gR2FtZU1hbmFnZXJcbkdhbWVNYW5hZ2VyIC0tfD4gVmlld1N0YXR1c1xuR2FtZU1hbmFnZXIgLS18PiBWaWV3SW52ZW50b3J5XG5HYW1lTWFuYWdlciAtLXw-IFN0b3JlXG5HYW1lTWFuYWdlciAtLXw-IER1bmdlb25cbkdhbWVNYW5hZ2VyIC0tfD4gQVBJTWFuYWdlclxuY2xhc3MgVGl0bGV7XG5zdHJpbmdbXSBtZW51SXRlbXM7XG5NYWluVGl0bGUoKVxufVxuY2xhc3MgQ2hhcmFjdGVye1xuaWQsIHB3LCBsZXZlbCwgZXhwLFxuam9iLCBkYW1hZ2UsLi4uLGludmVudG9yeVxufVxuY2xhc3MgQWNjb3VudHtcbkNoYXJhY3RlciBMb2dpbkNoYXJhY3RlclxuTGlzdCBJdGVtTGlzdFxuTG9naW4oKVxuQ3JlYXRlQWNjb3VudCgpXG5JbnB1dFBhc3N3b3JkKClcblNhdmVBY2NvdW50KClcblZlcmlmeVBhc3N3b3JkKClcbkhhc2hQYXNzd29yZCgpXG5DcmVhdGVTbGF0KClcbn1cbmNsYXNzIEl0ZW17XG5pdGVtTmFtZSwgaXRlbVR5cGUsXG5pdGVtc3RhdHVzLCBpdGVtRGVzYywgcHJpY2Vcbn1cbmNsYXNzIEdhbWVNYW5hZ2Vye1xuTGlzdCBJdGVtTGlzdFxuZW51bSBTZWxlY3RDb21tYW5kXG5zdHJpbmcgY2hhcmFjdGVyRmlsZVBhdGhcbnN0cmluZyBpdGVtRmlsZVBhdGhcbkdhbWVTdGFydCgpXG5TYXZlKClcbkRlbGV0ZUl0ZW1JbnZlbnRvcnkoKVxufVxuY2xhc3MgVmlld1N0YXR1c3tcbkNoYXJhY3RlciBjaGFyYWN0ZXJcbj0gQWNjb3VudC5Mb2dpbkNoYXJhY3RlclxuU3RhdHVzKClcbn1cbmNsYXNzIFZpZXdJbnZlbnRvcnl7XG5EaWN0aW9uYXJ5IF9vd25lZEl0ZW1zXG5JbnZlbnRvcnkoKVxuU2hvd0l0ZW1MaXN0KClcbkVxdWlwbWVudE1hbmFnZXIoKVxufVxuY2xhc3MgU3RvcmV7XG5TdG9yZVNob3AoKVxuU3RvcmVEaXNwbGF5KClcbkJ1eVN0b3JlKClcblNlbGxTdG9yZSgpXG59XG5jbGFzcyBEdW5nZW9ue1xuRHVuZ2VvbkxvYmJ5KClcbkR1bmdlb25FbnRyYW5jZSgpXG5EdW5nZW9uQ2xlYXJQcm9jZXNzKClcbkZhaWxEdW5nZW9uKClcbkNsZWFyRHVuZ2VvbigpXG5NYXBwaW5nRHVuZ2VvbkxldmVsKClcbkRlY3JlYXNlQ2hhcmFjdGVySHAoKVxuSW5jcmVhc2VHb2xkKClcbn1cbmNsYXNzIEFQSU1hbmFnZXJ7XG5Qb3N0IEl0ZW0oKVxuR2V0IEl0ZW0oKVxuRmV0Y2ggSXRlbSgpXG5HZXQgQWNjb3VudCgpXG5Qb3N0IEFjY291bnQoKVxuUG9zdCBTdGF0dXMoKVxuR2V0IFN0YXR1cygpXG59IiwibWVybWFpZCI6bnVsbH0)


### 기능 구성

#### 1. 계정 생성 및 로그인
- Character 구성은 Json File을 사용
- 암호는 Argon2를 이용한 단방향 암호화
#### 2.  아이템 장착 및 해제
- 별도의 Item Class 에서 Json 형태의 아이템 리스트 관리
- Character의 Inventory Class 에서 Dictionary의 Key Value로 아이템 장착 관리
#### 3. 던전 탐사 기능
- 던전 로비에서 케릭터 상태 확인 가능
- 던전 요구 방어도 이하 입장시 40% 확률로 실패하여 체력 감소
- 요구 던전 클리어 횟수 도달 시 레벨업 기능 
- 레벨업 시 능력치 상승
#### 4. 회복 기능
- Gold 지불 시 회복하여 HP 회복
#### 5. 상점 기능
- 구매 및 판매 기능 구현
#### 6. 온라인 채팅 기능 (구현중)
- Socket 사용하여 온라인 채팅 기능 구현

