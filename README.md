# 🎮 Tic-Tac-Toe Game

> A two-player Tic-Tac-Toe desktop game built with **C# and Windows Forms**, created as part of **Course 14 — C# Level 1** in the Programming Advices roadmap.
>
> This project focuses on practicing WinForms controls, events, `PictureBox`, enums, structs, methods, the `Tag` property, and game-state management.

---

## 🖼️ Application Preview

<img width="800" height="523" alt="Tic-Tac-Toe Game" src="https://github.com/user-attachments/assets/72dbcee5-a24e-4bb6-a3f7-acee1616d1d5" />

---

## 🚀 Project Overview

This project is a fully interactive **two-player Tic-Tac-Toe game** developed using C# and Windows Forms.

Two players play on the same computer:

* **Player 1** plays with `X`
* **Player 2** plays with `O`
* Players alternate turns automatically.
* The game checks for a winner after every move.
* Winning cells are highlighted.
* If all nine cells are occupied without a winner, the game ends in a draw.
* The game can be restarted at any time.

The main goal of the project is not only to create the game, but also to practice organizing game logic and connecting it with WinForms controls.

---

## 🏗️ Application Structure

```text
┌─────────────────────────────────────────────────┐
│                 Tic-Tac-Toe                     │
│                                                 │
│  Player Turn: Player1                           │
│                                                 │
│             [ ? ] | [ ? ] | [ ? ]              │
│             -------+-------+-------             │
│             [ ? ] | [ ? ] | [ ? ]              │
│             -------+-------+-------             │
│             [ ? ] | [ ? ] | [ ? ]              │
│                                                 │
│  Progress: In Progress                          │
│                                                 │
│              [ Restart Game ]                   │
└─────────────────────────────────────────────────┘
```

The game board consists of **9 PictureBox controls**:

```text
pictureBox1  pictureBox2  pictureBox3
pictureBox4  pictureBox5  pictureBox6
pictureBox7  pictureBox8  pictureBox9
```

The grid itself is drawn manually using the form's `Paint` event and `Graphics.DrawLine()`.

---

## ⚙️ Core Functionalities

| Feature                 | Description                                                             |
| ----------------------- | ----------------------------------------------------------------------- |
| Two-Player Mode         | Player 1 and Player 2 play on the same computer                         |
| Player Turns            | Players automatically switch between `Player1` and `Player2`            |
| X / O Images            | `X` and `O` images are loaded from project resources                    |
| Cell State              | Each PictureBox uses its `Tag` property to store `"?"`, `"X"`, or `"O"` |
| Winner Detection        | Checks all 8 possible winning combinations                              |
| Winning Highlight       | The three winning PictureBoxes are highlighted with `GreenYellow`       |
| Draw Detection          | A draw occurs when all 9 cells are played without a winner              |
| Invalid Move Prevention | Clicking an already occupied cell displays an error message             |
| Game Over Protection    | Moves are ignored after the game has ended                              |
| Restart Game            | Resets all cells and restores the initial game state                    |

---

## 🎨 Drawing the Game Board

The Tic-Tac-Toe grid is drawn directly on the form using the `Paint` event.

The code creates a white `Pen`, configures rounded line caps, and draws two horizontal and two vertical lines.

```csharp
private void Form1_Paint(object sender, PaintEventArgs e)
{
    Color Black = Color.White;

    Pen Pen = new Pen(Black);

    Pen.StartCap = LineCap.Round;
    Pen.EndCap = LineCap.Round;
    Pen.Width = 5;

    e.Graphics.DrawLine(Pen, 250, 170, 700, 170);
    e.Graphics.DrawLine(Pen, 250, 300, 700, 300);

    e.Graphics.DrawLine(Pen, 380, 70, 380, 400);
    e.Graphics.DrawLine(Pen, 530, 70, 530, 400);
}
```

This demonstrates how WinForms can be used to perform custom drawing with **GDI+** through the `Graphics` class.

---

## 🖼️ Images from Resources

The game uses images stored in the project's `Resources` file.

The three main images are:

* `Resources.X`
* `Resources.O`
* `Resources.question_mark_96`

When a player selects a cell, the corresponding image is assigned to the PictureBox.

```csharp
pic.Image = Resources.X;
```

or:

```csharp
pic.Image = Resources.O;
```

When the game is restarted:

```csharp
pictureBox.Image = Resources.question_mark_96;
```

Using project resources avoids loading the images from external files during runtime.

---

## 🏷️ Using the Tag Property as Cell State

Each PictureBox uses its `Tag` property to store the current state of the cell.

There are three possible values:

```text
"?" → Empty
"X" → Player 1
"O" → Player 2
```

For example:

```csharp
pic.Tag = "X";
```

The program checks the `Tag` values to determine whether a winning combination exists:

```csharp
if (pic1.Tag.ToString() != "?" &&
    pic1.Tag == pic2.Tag &&
    pic2.Tag == pic3.Tag)
{
    // Winner found
}
```

This allows the game logic to determine the state of a cell without depending on the image itself.

---

## 👤 Player Management with Enum

The current player is represented using the `enPlayer` enum.

```csharp
enum enPlayer
{
    Player1,
    Player2
}
```

The initial player is:

```csharp
enPlayer PlayerTurn = enPlayer.Player1;
```

After Player 1 plays, the turn changes to Player 2:

```csharp
PlayerTurn = enPlayer.Player2;
```

After Player 2 plays, it changes back:

```csharp
PlayerTurn = enPlayer.Player1;
```

Using an enum makes the code more readable than using values such as `0` and `1`.

---

## 🏆 Winner State with Enum

The game uses another enum to represent the result of the game.

```csharp
enum enWinner
{
    Player1,
    Player2,
    Draw,
    GameInProgress
}
```

The game can therefore have four possible states:

```text
Player1
Player2
Draw
GameInProgress
```

For example, when Player 1 wins:

```csharp
GameStatus.Winner = enWinner.Player1;
```

When the game starts or is restarted:

```csharp
GameStatus.Winner = enWinner.GameInProgress;
```

---

## 📦 Managing Game State with a Struct

The current game state is grouped inside the `stGameStatus` struct.

```csharp
struct stGameStatus
{
    public enWinner Winner;
    public bool GameOver;
    public short PlayCount;
}
```

The structure contains three important pieces of information:

| Property    | Purpose                                            |
| ----------- | -------------------------------------------------- |
| `Winner`    | Stores Player 1, Player 2, Draw, or GameInProgress |
| `GameOver`  | Determines whether the game has ended              |
| `PlayCount` | Counts the number of moves played                  |

The game creates one variable to hold this state:

```csharp
stGameStatus GameStatus;
```

Because this is a struct, its fields receive their default values automatically.

For example:

```text
GameOver  → false
PlayCount → 0
```

The game later updates these values as the players make moves.

---

## 🎯 Changing a PictureBox

The main method responsible for playing a move is:

```csharp
public void ChangeImage(PictureBox pic)
```

Instead of creating separate game logic for every PictureBox, the selected PictureBox is passed to the same method.

First, the method checks whether the game has already ended:

```csharp
if (GameStatus.GameOver)
    return;
```

Then it checks whether the selected cell is empty:

```csharp
if (pic.Tag.ToString() == "?")
```

If the cell is available, the current player's image and state are assigned.

For Player 1:

```csharp
pic.Image = Resources.X;
pic.Tag = "X";
```

For Player 2:

```csharp
pic.Image = Resources.O;
pic.Tag = "O";
```

The move counter is also increased:

```csharp
GameStatus.PlayCount++;
```

Finally, the program checks whether the move produced a winner:

```csharp
CheckWinner();
```

---

## 🔄 Reusing the Same Logic for PictureBoxes

The game passes the selected `PictureBox` to `ChangeImage()`.

For example:

```csharp
private void pictureBox1_Click(object sender, EventArgs e)
{
    ChangeImage(pictureBox1);
}
```

Another PictureBox:

```csharp
private void pictureBox2_Click(object sender, EventArgs e)
{
    ChangeImage(pictureBox2);
}
```

And so on.

The important idea is that the game logic exists in **one reusable method**:

```csharp
ChangeImage(PictureBox pic)
```

The method doesn't need to know which PictureBox was clicked beforehand. It receives the selected control as a parameter.

For example:

```text
pictureBox1_Click
       ↓
ChangeImage(pictureBox1)

pictureBox2_Click
       ↓
ChangeImage(pictureBox2)

pictureBox9_Click
       ↓
ChangeImage(pictureBox9)
```

This avoids duplicating the actual game logic nine times.

---

## 🏆 Winner Detection

After every move, the program calls:

```csharp
CheckWinner();
```

This method checks all possible winning combinations.

There are exactly **8 possible winning combinations** in Tic-Tac-Toe:

### Rows

```text
1 2 3
4 5 6
7 8 9
```

The code checks:

```csharp
CheckValues(pictureBox1, pictureBox2, pictureBox3);
CheckValues(pictureBox4, pictureBox5, pictureBox6);
CheckValues(pictureBox7, pictureBox8, pictureBox9);
```

### Columns

```text
1 4 7
2 5 8
3 6 9
```

The code checks:

```csharp
CheckValues(pictureBox1, pictureBox4, pictureBox7);
CheckValues(pictureBox2, pictureBox5, pictureBox8);
CheckValues(pictureBox3, pictureBox6, pictureBox9);
```

### Diagonals

```text
1 5 9
3 5 7
```

The code checks:

```csharp
CheckValues(pictureBox1, pictureBox5, pictureBox9);
CheckValues(pictureBox3, pictureBox5, pictureBox7);
```

---

## ♻️ Reusable CheckValues Method

Instead of writing the same winner-checking logic eight times, the project uses one reusable method:

```csharp
bool CheckValues(
    PictureBox pic1,
    PictureBox pic2,
    PictureBox pic3)
```

The method verifies three things:

1. The first cell is not empty.
2. The first and second cells have the same value.
3. The second and third cells have the same value.

```csharp
if (pic1.Tag.ToString() != "?" &&
    pic1.Tag == pic2.Tag &&
    pic2.Tag == pic3.Tag)
{
    // Winner
}
```

If the three cells contain `X`, Player 1 wins:

```csharp
if (pic1.Tag.ToString() == "X")
    GameStatus.Winner = enWinner.Player1;
```

Otherwise, Player 2 wins:

```csharp
else
    GameStatus.Winner = enWinner.Player2;
```

The game is then marked as over:

```csharp
GameStatus.GameOver = true;
```

---

## 🟢 Highlighting the Winning Cells

When a winning combination is found, the three PictureBoxes are highlighted:

```csharp
pic1.BackColor = Color.GreenYellow;
pic2.BackColor = Color.GreenYellow;
pic3.BackColor = Color.GreenYellow;
```

This provides immediate visual feedback to the players.

---

## 🛑 Ending the Game

When a player wins, `EndGame()` is called.

```csharp
void EndGame()
{
    lblPlayerTurn.Text = "Game Over";

    switch (GameStatus.Winner)
    {
        case enWinner.Player1:
            lblProgress.Text = "Player1";
            break;

        case enWinner.Player2:
            lblProgress.Text = "Player2";
            break;

        case enWinner.Draw:
            lblProgress.Text = "Draw";
            break;
    }

    MessageBox.Show(
        "Game Over",
        "Game Over Draw",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
}
```

The method updates the UI according to the final game result and displays a message to the players.

---

## 🤝 Draw Detection

A Tic-Tac-Toe game can also end without a winner.

After every move, the program checks:

```csharp
if (GameStatus.PlayCount == 9 &&
    !GameStatus.GameOver)
{
    GameStatus.GameOver = true;
    GameStatus.Winner = enWinner.Draw;
    EndGame();
}
```

If all 9 cells have been played and no winner has been found, the game is declared a draw.

---

## ❌ Invalid Move Prevention

A player cannot play in a cell that has already been selected.

Each empty PictureBox has:

```text
Tag = "?"
```

Once a player uses it, its Tag becomes either:

```text
"X"
```

or:

```text
"O"
```

If the player clicks the cell again, the program displays an error:

```csharp
MessageBox.Show(
    "Wrong Choice",
    "Worng",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error);
```

This prevents a player from overwriting an existing move.

---

## 🔒 Game Over Protection

The first line of `ChangeImage()` prevents players from making additional moves after the game ends:

```csharp
if (GameStatus.GameOver)
    return;
```

Therefore, once a winner or draw has been detected, clicking the board no longer changes the game.

---

## 🔄 Restarting the Game

The project includes a `Restart Game` button.

The restart process is handled by:

```csharp
void RestartGame()
```

Each PictureBox is reset using:

```csharp
void ResetPictureBox(PictureBox pictureBox)
{
    pictureBox.Image = Resources.question_mark_96;
    pictureBox.BackColor = Color.Transparent;
    pictureBox.Tag = "?";
}
```

All nine cells are then reset.

The game state is also restored:

```csharp
GameStatus.GameOver = false;
GameStatus.PlayCount = 0;

PlayerTurn = enPlayer.Player1;

GameStatus.Winner = enWinner.GameInProgress;
```

The UI is updated:

```csharp
lblPlayerTurn.Text = PlayerTurn.ToString();
lblProgress.Text = "In Progress";
```

Therefore, the game returns to its initial state and Player 1 starts again.

---

## 🧠 Key Technical Concepts Practiced

### C# Concepts

* `enum`
* `struct`
* Methods
* Method parameters
* `switch`
* `if` conditions
* Boolean state
* Increment operators
* Default values of struct fields

### Windows Forms Concepts

* `Form`
* `Paint` event
* `PictureBox`
* `Click` events
* `Label`
* `MessageBox`
* `BackColor`
* `Image`
* `Tag`
* `sender`
* Passing controls as method parameters

### Game Logic Concepts

* Managing player turns
* Tracking game state
* Detecting winning combinations
* Detecting draws
* Preventing invalid moves
* Ending a game
* Restarting a game
* Reusing methods instead of duplicating logic

---

## 📚 What This Project Taught Me

Through this project, I practiced how to connect **C# logic with Windows Forms controls**.

The most important concepts I practiced were:

1. **Using `Tag` to store the state of a UI control.**
2. **Passing a `PictureBox` to a reusable method.**
3. **Using enums to represent player and winner states.**
4. **Using a struct to group related game-state information.**
5. **Using one reusable method to check different winning combinations.**
6. **Managing game flow with a `GameOver` flag and `PlayCount`.**
7. **Using the `Paint` event and `Graphics.DrawLine()` to draw the board.**
8. **Using project resources to display X, O, and empty-cell images.**
9. **Handling WinForms events through `Click` event handlers.**

---

## 🏁 Course Context

This project is part of:

**Course 14 — C# Level 1**
**Stage Two — Universal Programming Foundations**
**Programming Advices Roadmap**

The project was created to practice C# fundamentals together with Windows Forms and to understand how application logic can interact with graphical controls.

> The goal of the project is learning and practicing programming concepts rather than building the most advanced or optimized Tic-Tac-Toe implementation.

---

## 🙏 Credits

This project was created as part of my learning journey with:

* **Programming Advices**
* **Dr. Mohammed Abu-Hadhoud**

Official platform:

[Programming Advices](https://programmingadvices.com?utm_source=chatgpt.com)

---

## 🚀 What's Next?

This project is another step in strengthening my C# and programming fundamentals.

The next projects will focus on applying these concepts to larger applications, improving code organization, and gradually moving toward more advanced C# and .NET development.
