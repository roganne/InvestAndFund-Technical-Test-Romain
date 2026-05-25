
# Senior Developer Refactoring Exercise

## Overview

This exercise is based on a **core order‑processing component** that is relied upon by multiple teams.

The code currently works and is stable in production, but recent changes have made it **difficult to extend safely**. The design is tightly coupled, and changes can often introduce unintended side effects. A new business requirement has exposed these issues and is the main driver for this refactor.

## Your Task

Refactor the existing code to:

- Identify the major design and maintainability issues
- Refactor towards **SOLID principles**
- Introduce **appropriate design patterns** where they add value
- Support the new `Loyal` customer type cleanly, described below.

You **do not** need to redesign the entire system. Focus on **high‑value improvements** that reduce risk and improve changeability.
## New Requirement 
A new customer type is being introduced:

- CustomerType = Loyal
- Loyal customers receive a 50% discount

This change must be added without increasing risk or making the code harder to maintain.

## Explicit Out of Scope

The following methods **do not require real implementations**:

- `ChargeCard`
- `ProcessBankTransfer`
- `SaveToDatabase`
- `SendEmail`

You **do not** need to:
- Implement database access
- Implement email delivery
- Integrate real payment providers

## Timebox

- Approximately **1 hour**
- Focus on **design quality and reasoning**, not completeness
- Depth and clarity are more important than finishing everything

There is no single correct solution. We care about **quality of thinking and approach**.

## To Upload Your Solution

1. Set-up an account on GitHub or use your existing account. 
2. Create a public repository called "InvestAndFund Technical Test – {name}" replacing {name} with your own. 
3. Or create a zip file of your solution and send via email.

