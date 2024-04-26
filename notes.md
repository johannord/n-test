# Approach

1. Make runnable
2. Add tests (track regression)
    Assuming 2013 (e.g. holidays) based on existing code
3. Solution structure
4. Refactor
5. Improvements (to do if continuing)

    - Refactor, hourly rate tariffs to be modelled using TimeOnly instead of if logic
    - Refactor, separate out 60 min rule (separated calculation logic)
    - Refactor, separate out total tax / day rule (separated calculation logic)
    - Refactor, make tax calculator logic manage input spaning over multiple dates -> output tax amount / date
