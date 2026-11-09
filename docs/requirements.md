# requirements.md

## Problem statement

Our project partners want to present a piece of their motion-tracking
technology in a game setting so that clients and interested parties can
demo the technology in an interactive and engaging way that effectively
showcases its capabilities and depth.

## Functional requirements

-   **FR-1 (Must):** Players fight enemies in interactive 1-on-1 matches
    using their bodies.\
    **Acceptance:** A full, functional, completable fight.

-   **FR-2 (Must):** Players can clearly see how motion tracking is
    utilized and translated into the game.

-   **FR-3 (Must):** The system detects and handles tracking or bounds
    errors during play.

## Non-functional requirements

-   **NFR-1 (Must, assumed):** Motion tracking lags or fails less than
    1% of the time during play.
-   **NFR-2 (Should, assumed):** 99% of in-game actions work through
    motion-tracked input alone (i.e., no additional controllers are
    required).
-   **NFR-3 (Must):** Players can comfortably endure the length of the
    one-minute fights.

## Constraints

-   **C-1:** All necessary movements can be performed and tracked within
    the dictated play space (5 sq. ft.).
-   **C-2:** The game is fully testable without project partner
    technology until it becomes available.
-   **C-3:** No data regarding the motion-capture technology used may
    leave the partner's network or the capstone class's instruction
    team.

## Success metrics

-   Maximum time needed to fully demo the technology with the game:
    **less than 2 minutes**.
-   Time to start the game (excluding setup of the play environment):
    **less than 30 seconds**.

## Non-goals

-   A fully playable experience without the project partner's technology
    (via XR, VR, etc.).
-   Additional modes or games that also showcase the technology after
    the project is fully completed.

## Open questions

-   **What frame rate is required for responsive gameplay?**\
    **Owner:** Brett\
    **Target:** End of Fall term

-   **What is the acceptable latency between physical movement and
    visible gameplay response?**\
    **Owner:** Dylan\
    **Target:** End of Winter term
