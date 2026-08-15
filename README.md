# 🎮 Project 24 — Tic-Tac-Toe Game

> Second WinForms desktop application — a fully interactive two-player Tic-Tac-Toe game built with C# and .NET Framework as part of Course 14 (C# Level 1) in the Programming Advices roadmap.

---

## 🖼️ Application Preview

<img width="800" height="523" alt="image" src="https://github.com/user-attachments/assets/72dbcee5-a24e-4bb6-a3f7-acee1616d1d5" />

---

## 🚀 Project Overview

This project is not just a game.

It is a practical exercise in connecting controls together,
understanding how events flow through a form,
and learning techniques that reduce code dramatically.

The game supports two players on the same machine.
Each player places their symbol in turns.
The game detects winners, handles draws, highlights winning cells,
and resets cleanly with a single button.

---

## 🏗️ Application Structure

```
┌──────────────────────────────────────────────────┐
│              Tic-Tac-Toe Game                    │
│                                                  │
│  Turn         [ ? ] | [ ? ] | [ ? ]              │
│  Player 1     ──────────────────                 │
│               [ ? ] | [ ? ] | [ ? ]              │
│  Winner       ──────────────────                 │
│  In Progress  [ ? ] | [ ? ] | [ ? ]              │
│                                                  │
│  [ Restart Game ]                                │
└──────────────────────────────────────────────────┘
```

The game grid is drawn manually on the form using
`Graphics.DrawLine()`
with a custom white pen and rounded line caps.

---

## ⚙️ Core Functionalities

| Feature | Description |
|---|---|
| Two-Player Mode | Players alternate turns on the same machine |
| Turn Indicator | Live label showing whose turn it is |
| Winner Detection | Checks all 8 win combinations after every move |
| Draw Detection | Triggers when all 9 cells are filled with no winner |
| Visual Win Highlight | Winning cells change to GreenYellow |
| Invalid Move Prevention | Wrong cell click triggers a warning MessageBox |
| Disable After Game Over | All buttons disabled when game ends |
| Restart Button | Resets all cells, labels, and state instantly |

---

## 🧠 Key Technical Concepts

### Custom Form Drawing
The grid lines are drawn directly on the form using the
`Paint`
event and
`Graphics.DrawLine()`.

```csharp
Pen WhitePen = new Pen(Color.White);
WhitePen.Width = 13;
WhitePen.StartCap = LineCap.Round;
WhitePen.EndCap = LineCap.Round;

e.Graphics.DrawLine(WhitePen, 615, 160, 615, 615); // vertical line 1
e.Graphics.DrawLine(WhitePen, 415, 310, 1050, 310); // horizontal line 1
```

---

### Images Loaded from Resources
X, O, and QuestionMark images are embedded in the project
`Resources`
and assigned directly to button images.

```csharp
btn.Image = Resources.QuestionMark;
btn.Image = Resources.X;
btn.Image = Resources.O;
```

---

### Tag Property for Cell State
Each button stores its current state in the
`Tag`
property — either
`"?"`, `"X"`, or `"O"`.

This removes the need for a separate array and connects the UI directly to the logic.

```csharp
btn.Tag = "X";
if (btn1.Tag.ToString() == btn2.Tag.ToString()) { ... }
```

---

### Single Event Handler for All 9 Buttons
Instead of writing 9 separate click handlers, all buttons share one method.

The
`sender`
parameter is cast to
`Button`
and passed to
`ChangeImage()`.

```csharp
// All 9 buttons point to the same event
private void button_Click(object sender, EventArgs e)
{
    ChangeImage((Button)sender);
}
```

This is one of the most important techniques taught in this project.

Any control that triggers an event passes itself as
`sender`.
Casting it gives full access to that control — no need to know which button was clicked.

---

### Enums and Struct for Game State
Game state is organized cleanly using enums and a struct.

```csharp
enum enPlayerTurn  { Player1, Player2 }
enum enWinner      { Player1, Player2, Draw, GameInProgress }

struct stGameStatus
{
    public enWinner Winner;
    public byte PlayCount;
    public bool GameOver;
}
```

---

### Win Detection — 8 Combinations
All 8 possible win paths are checked after every move
using a single reusable function.

```csharp
private bool CheckValues(Button btn1, Button btn2, Button btn3)
{
    if (btn1.Tag.ToString() != "?" &&
        btn1.Tag.ToString() == btn2.Tag.ToString() &&
        btn1.Tag.ToString() == btn3.Tag.ToString())
    {
        btn1.BackColor = Color.GreenYellow;
        btn2.BackColor = Color.GreenYellow;
        btn3.BackColor = Color.GreenYellow;
        // set winner and end game
        return true;
    }
    return false;
}
```

The function is called 8 times — rows, columns, and diagonals.

---

### Controls as Parameters
Functions that modify controls accept
`Button`
as a parameter.

Since controls are reference types, any change inside the function
affects the actual control on the form — no return value needed.

```csharp
private void ResetButton(Button btn)
{
    btn.Enabled = true;
    btn.Image = Resources.QuestionMark;
    btn.Tag = "?";
    btn.BackColor = Color.Transparent;
}
```

---

## 🎯 What This Project Teaches

- Drawing directly on a WinForms form using
`GDI+`
- Embedding and using images from project resources
- Using the
`Tag`
property to bind state to controls
- Sharing a single event handler across multiple controls
- Casting
`sender`
to access the triggering control
- Passing controls as function parameters
- Enums and structs for clean state management
- Win detection logic across rows, columns, and diagonals

---

## 🏁 Course Context

This project is part of:

**Course 14 — C# Level 1**
**Stage Two — Universal Programming Foundations**
**Programming Advices Roadmap**

It is the second WinForms project in Stage Two,
built specifically to practice control interconnection,
event sharing, and technique-driven thinking.

> The solution is not the most optimal —
> it is designed to teach how controls connect,
> how events flow, and how techniques reduce complexity.

---

## 🙏 Gratitude

- **Programming Advices Platform**
- **Dr. Mohammed Abu-Hadhoud**
- **[https://programmingadvices.com](https://programmingadvices.com)**

---

## 🔗 LinkedIn Post

> Full demo video and details available on LinkedIn:
> [View Post](https://www.linkedin.com/posts/ahmed-yaser-mt_csharp-dotnet-winforms-ugcPost-7460771546198900736--WAc?utm_source=share&utm_medium=member_desktop&rcm=ACoAAFPMIswBcvLoTHZvRE41ps8H-E1OpPUyxeo)

---

## 🚀 What's Next?

Stage Two continues.
More projects. More concepts. Deeper foundations.
