# RFC: Game Engine

| Author | Skylar Green |
| Date | 2026-10-25 |
| Status | Accepted |
| Decision Date | 2026-10-12 |
| Deciders | Project Team and Project Partner |
| Superseded By | N/A |

## Problem and Context

We are developing a Video Game where players fight giant monster enemies as a giant robot mech. Before any work can be made on this project we first need to decide which game engine we will use to develop the game. The game engine acts as the game's foundation and its workshop. Many game engines share common features, but they each have unique tools and specialties that can affect how easy or difficult a game is to make.

It is imperative that we decide on which game engine to use as soon as possible because before that decision is made no other work relating to the game's assets or codebase can begin. If we procrastinate this decision, we delay how quickly we can begin making our project tangible, which delays the project as a whole.

## Options

### Option A: Unity

Unity is a free game engine available to everyone. It's fairly simple and beginner friendly which is good for any team member who may not have used it before. Unity is also very good at handling 3D games, which fits our project. The cost of using Unity is low to none as there is no subscription fee and a smaller barrier to entry than other engines. Unity is also well suited for smaller 3D games.

### Option B: Unreal

Unreal is another free game engine available to anyone. It can be more complicated than something like Unity and the file sizes are much larger. Unreal is adept at making highly detailed, large-scale games. The cost of choosing Unreal is a noticeably higher, as it's not as beginner friendly as the other options and the large-scale file sizes aren't as relevant for our project, which is supposed to be a smaller-scale demo game.

### Option C: Godot

Just like the other engines, Godot is free, but it's also open source. Godot is a very beginner friendly game engine that generally favors 2D style games. Because Godot is open source the ceiling of what it can be used for is very high, however, most of our members have little to no experience using Godot, and it could be costly to try and customize Godot to best fit our project. 

### Option D: Do Nothing

Abstaining from choosing a game engine delays everything. We can't begin building a codebase structure or game assets without knowing what kind of files will be involved. We can't begin building a codebase without knowing what language we'll be writing it in. Choosing a game engine is one of the first decisions to be made when developing a video game and the cost of not making that decision is immediate. However long it takes to choose a game engine is exactly how much longer it will take to start making the project tangible.

## Analysis

The technical tradeoffs that matter for this decision: performance, complexity, maintainability, security, cost. Argue from evidence (benchmarks, prototypes, documentation, prior art) and cite what you relied on so a reviewer can check it. Acknowledge what you are uncertain about.



## Recommendation and Verification Plan

Which option you propose and why it wins the analysis above. Then, concretely: how will the team know it worked? List the tests, metrics, or acceptance criteria that would detect failure, and when they will be checked.

## Constraints

Licensing, data use, IP, and the partner confidentiality boundary as they bear on this decision. If none apply, say so explicitly and briefly explain why.

## Delegation and Validation

- **What you handed to AI tools** on this decision, whether that was the analysis, the prototype, the tests, or the writing.
- **What checks made that reasonable**: tests, CI, review gates, a staging environment, a rollback path. Name the ones that actually exist in your repo, not the ones you intend to build.
- **Where you stopped and validated the result yourself, and why those points.** If this decision is hard to reverse, say what you personally checked before committing to it.

What is assessed is in the [AI policy](https://capstone.alexulbrich.com/assignments/introduction/#ai-policy-applies-to-every-assignment).

## Decision

Proposed, until your team decides. Then accepted, rejected, or superseded, with the date and who chaired the decision. If superseded, link the RFC that replaces this one.
