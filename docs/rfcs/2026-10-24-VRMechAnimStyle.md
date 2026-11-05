<!-- docs/rfcs/2026-10-24-VRMechAnimStyle.md (or docs/adr/, whichever your repo uses) -->

# RFC: VR Mech Animation Style

| Author | Brett Thompson |
| Date | 2026-10-24 |
| Status | Accepted
| Decision Date | 2026-10-24 |
| Deciders | (who agreed to the outcome) |
| Superseded By | (link, if applicable) |

## Problem and Context

This is a VR game where the player will be boxing giant mechs and kaijus (a la Pacific Rim or Gundam) Since we want the player’s hand movements to closely match their in game avatar, how we animate the enemies will be tricky, to avoid pushover enemies or significant inconsistencies.

We need to decide how we are going about animating our enemy characters in relation to our player. If we aren’t consistent with the implementation, or what we start with doesn’t work, it will be difficult to pivot to a new animation style.

## Options


### Option A: Key Framing

Enemies are given set animations that they switch between given environmental changes and programmed behavior. This lets us control movements more precisely and prevents enemies from becoming ragdolls. However this would restrict the possibilities for the player, especially when it comes to blocking.

### Option B: Physics + Inverse Kinematics

Record movements and use IK to convert them to forces applied to the enemy model. Then these get summed with the forces the player applies to the enemies and the physics engine handles the rest. This allows for more freedom for the player in blocking their moves. However this does involve more calculations which would need to be tested, and can result in the enemy acting like a ragdoll (especially since the player is unmovable by them).

### Option C: Do Nothing

No clear direction on animation, can’t make animations until decided. Initial beta test may be against a mostly static punching bag.


## Analysis

Player Freedom - Physics based provides more freedom
Testability – Key-framing is more easily testable
Number of Animations – Physics based would have fewer animations
Quality – Key-framing would have better looking animations

## Recommendation and Verification Plan

We propose key-framing since it is easier to test and would end up looking more polished easier. Player freedom could be mostly solved with a few extra animations, and it would be easier to write a tutorial for the game. Unit tests will check that models are hooked up properly and have all their animations available. If any fields or references are missing or improperly configured we will know the root cause of whatever visual glitch it caused. Integration tests will check whether enemies cleanly transition between their various animations. A enemy should be able to play each of it’s animations. And user testing will ensure that the animations are smooth and fit with the game

## Constraints

Licensing, data use, IP, and the partner confidentiality boundary as they bear on this decision. If none apply, say so explicitly and briefly explain why.

## Delegation and Validation

- no AI use
- Tests, staging branch, CI
- I made the enemy behavior tree to check what animations need to be made, and how they will be switched between

## Decision

Accepted
