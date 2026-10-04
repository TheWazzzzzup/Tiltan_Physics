# Tiltan Physics

A 2D pool/billiards game built on a custom physics engine written from scratch, rather than relying on Unity's built-in Physics2D. Implements Separating Axis Theorem (SAT) collision detection for circle-circle, box-box, and mixed box-circle intersections, with a custom rigidbody component handling velocity integration, drag-based deceleration, and static/dynamic/trigger collision resolution.

Game logic sits on top via a turn-based manager and a ScriptableObject event and runtime-set architecture for decoupled systems. Built as a deep dive into 2D physics and collision math beyond what Unity provides out of the box.
