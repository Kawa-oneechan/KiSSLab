# Scripting

Scripting is based on the [FKiSS standard](http://tigger.orpheusweb.co.uk/KISS/fkref4.html), reworked into a Lisp style. Several events are legal to specify, but only the listed ones actually trigger. All in all, the system is just about 

### Events

| Form                                  | Example                            | Description                                                  |
| ------------------------------------- | ---------------------------------- | ------------------------------------------------------------ |
| `(alarm <number, string, or symbol>)` | `(alarm "blink")`<br />`(alarm 1)` | A timer reaches zero.                                        |
| `(begin)`                             | `(begin)`                          | After `(initialize)`. May be deprecated.                     |
| `(catch <cel or part>)`               | `(catch "thing")`                  | The user clicks on the part or cel. Applies to all cels and parts, except those with a maximal fix value. |
| `(col <number>)`                      | `(col 2)`                          | The current color palette was changed to a particular value. May change to an argument-less format with a meta-variable. |
| `(collide <part> <part>)`             | `(collide "jacket" "body")`        | The two parts ~~or cels~~ touch, taking transparent pixels into account. Triggers only if they did not overlap before one of them was moved by the user. The variables `#A` and `#B` are set for your convenience. |
| `(drop <cel or part>)`                | `(drop "thing")`                   | The user releases the mouse on the part or cel. Applies to all cels ands parts, except those with a maximal fix value. |
| `(fixcatch <cel or part>)`            | `(fixcatch "thing")`               | The user clicks on the part or cel. Applies only to fixed parts. |
| `(fixdrop <cel or part>)`             | `(fixdrop "thing")`                | The user releases the mouse on the part or cel. Applies only to fixed parts. |
| `(in <part> <part>)`                  | `(in "jacket" "body")`             | The two parts ~~or cels~~ overlap, ignoring transparency.<br/>Triggers only if they did not overlap before one of them was moved by the user. The variables `#A` and `#B` are set for your convenience. |
| `(initialize)`                        |                                    | Before the doll is displayed after loading.                  |
| `(keypress <key>)`                    | `(keypress a)`                     | The user pressed down a particular key. May change to an argument-less format with a meta-variable. |
| `(keyrelease <key>)`                  | `(keyrelease right)`               | The user released a particular key. May change to an argument-less format with a meta-variable. |
| `(mousein <cel or part>)`             | `(mousein "thing")`                | The user placed the mouse cursor inside the part or cel.     |
| `(mouseout <cel or part>)`            | `(mouseout "thing")`               | The user moved the mouse cursor away from the part or cel that it was previously on. |
| `(press <cel or part>)`               | `(press "thing")`                  | The user clicks on the part or cel.<br/>Applies to all cels and parts regardless of fix value. |
| `(release <cel or part>)`             | `(release "thing")`                | The user releases the part or cel.<br/>Applies to all cels and parts regardless of fix value. |
| `(set <num>)`                         | `(set 4)`                          | The user switched to the given set. May change to an argument-less format with a meta-variable. |
| `(unfix <cel or part)`                | `(unfix "thing")`                  | A part that had a non-zero fix value reached zero and is now loose. |

### Types

In the function list, the following types are used:

| Name          | Example     | Description                                                  |
| ------------- | ----------- | ------------------------------------------------------------ |
| `bool`        | `true`      | The symbol `true` or `false`, or a number, where 0 is `false` and 1 or higher is `true`, or a list, where the empty list is `false`. |
| `cel or part` |             | The ID of a cel or part, or an object reference to a cel, part, or list of either. |
| `cel`         | `"bangs1"`  | The ID of a cel, or an object reference to a cel or list of cels. |
| `list`        |             | An object reference to a list.                               |
| `number`      | `47` `aVar` | Any expression that evaluates into a numerical value. This can be be an actual number, a variable with a number value, or a function call that returns a number. |
| `object`      |             | An object reference.                                         |
| `variable`    | `aVar`      | A symbol whose name is not that of a function.               |

### Object references

As a .Net application, the doll parts and cels are implemented as .Net classes. Script functions like `part` return these objects as-is, and functions like `unmap` can accept them as arguments.

This means, of course, that these objects have properties, and you can access them: `(<objRef> <property>:)` will return the value of the specified property, and `(<objRef ><property>: <value>)` will set it, assuming the value is of the correct type -- you can't do `(pants fix: "hello")`, because the fix value is a number. You can also have multiple "sends" in a row, both to get and to set, so `(pants fix: 16 cels:)` will first set the fix value, then return the list of component cels, which is itself an object reference.

### Functions

`(<operator> <numbers>)`

The math operations `+`, `-`, `/`, and `*` are available, and take any number of operands, which may be variables with numerical values, or function calls that return numbers, and then returns that number. The `%` modulo operator takes only two operands, while `++` and `--` take only one operand, a variable, and return that same variable.

`(= <variable> <anything>)`

Sets the specified variable, a symbol that is not already a command's name, to any legally-expressible value. Though `true` and `false` are technically variables, they are read-only. There's also `%=`, which takes a variable and an operand, then sets that variable to itself modulo the operand.

`(<operator> <number> <number>)`

The comparison operations `==`, `!=`, `<`, `<=`, `>`, and `>=` are available, and compare any two expression that evaluate to numbers with each other, returning `true` or `false` accordingly.

`(and <numbers>)`

Returns `true` if *all* operands are truthy. Evaluates each operand in turn until one is not truthy. See also `or`.

`(altmap <cel or part>)`

By the same rules as `map`, all target cels have their visibility toggled.

`(cat <strings>)`

Returns all given arguments joined together.

```clojure
(= greeting "Hello")
(= greeting (cat greeting " World"))
(notify greeting) ; "Hello World"
```

`(cel? <object>)`

Returns `true` if the argument is a cel object reference, `false` otherwise.

`(cels <cel>)`

Since string IDs can refer to both cels and parts, and one ID can refer to either at once, the `cels` command can be used to *explicitly* return a cel object reference for the cel with that name. It may also return a list object reference if that ID refers to multiple cels, which is also a thing that can happen.

`(colliding? <cel or part> <cel or part>)`

Returns `true` if the items overlap on a pixel level.

`(cond <conditions>)`

A Lisp staple, sort of like a chain of `if`-`else if` statements.

```clojure
(cond
    ((== i 2)
        ; Do something in this case, then stop.
    )
    ((== i j)
        ; Do something else.
    )
    (else
        ; None of the above conditions held true.
    )
)
```

`(for (<var> <from> <to> <optional step>) ...)`

Iterates from one value to the next, sets the specified variable to each number in turn and executes the statements within. If the step value is left out, 1 or -1 is implied depending on whether the "from" value is lower or higher than the "to".

```clojure
(for (i 1 10 2)
	(notify i) ; Counts 1, 3, 5, 7, 9.
)
```

`(foreach (<list> <var>) ...)`

Given a list object reference, sets the specified variable to each list item in turn and executes the statements inside.

```clojure
; Displays the names of the component cels of the "body" object.
(= objectRef (part "body")) ; Get the "body" part as an object reference.
(= theCels (objectRef cels:)) ; Do a property call to get a list of cels.
(foreach (theCels c)
	(notify c)
)
```

`(ghost <cel or part> [<bool>])`

By the same rules as `map`, all target cels are made utterly unclickable. If a `true` or `false` is given, or an expression that evaluates as such, they'll be made unclickable if `true`, otherwise they'll start responding to clicks again.

`(if <expression> ...)`

Given an expression that evaluates to a number, executes the statements inside if that number is truthy (1 or higher). You can use an `else` symbol to specify what should happen otherwise.

```clojure
(if (mapped? "thing")
	(notify "The thing is mapped.")
	; Could do more things if we wanted.
else
	(notify "The thing is NOT mapped.")
)
```

`(inside? <cel or part> <cel or part>)`

Returns `true` if the items overlap on a bounding box level.

`(list? <object>)`

Returns `true` if the argument is a list object reference, `false` otherwise.

`(map <cel or part>)`

If the argument is a cel or list of them, turns them visible. If it's a part, all of its component cels are mapped.

`(mapped? <cel or part>)`

If the argument is a cel, returns `true` if it's mapped, `false` otherwise. If the argument is a part, returns `true` only if *all* its component cels are mapped.

`(moverel <part> <part> <number> <number>)`

Basically a shorthand for FKiSS' `movebyx`/`movebyy` commands, moves the first part somewhere relative to the second. The parts may be left out, in which case the `#A` and `#B` variables are assumed.

`(moveto <part> <number> <number>)`

Moves the given part to a new absolute position. The part may be left out, in which case the `#A` variable is assumed.

`(music <media filename>)`

Causes the given sound file to play in the background, on a loop, until another song is requested. If no argument is given, stops whatever music is already playing.

`(not <number>)`

Returns 1 if the number given is 0, or 0 if the number is 1 or higher.

`(nul? <object>)`

Returns `true` if the argument is a null object reference, or an empty list, `false` otherwise.

`(notify <string>)`

Displays the given string (or string representation of any value, really) in a message box.

`(nummapped <cel or part>)`

If the argument is a cel, returns 1 if it's mapped, 0 otherwise. If the argument is a part, returns the number of component cels that are mapped.

`(or <numbers>)`

Returns `true` if *any* operands are truthy. Evaluates each operand in turn until one is truthy. See also `and`.

`(part <part>)`

Since string IDs can refer to both cels and parts, and one ID can refer to either at once, the `part` command can be used to *explicitly* return a part object reference for the part with that name. Unlike `cels`, this will never return a list.

`(part? <object>)`

Returns `true` if the argument is a part object reference, `false` otherwise.

`(random <number> <maybe another number>)`

If one number is given, returns a random value from 0 to that number, exclusive. If two are given, returns a number between those two.

`(range <number> <higher number>)`

Returns a list with all the numbers between the two given, inclusively.

`(sound <media filename>)`

Causes the given sound file to play, once. Returns a sound object reference.

`(stopsound <sound object reference>)`

Stops the given sound.

`(switch <number> <conditions>)`

Given an expression and a list of values, finds the first condition that matches.

```clojure
(switch i
    (2
        ; Do something if (== i 2) is true, then stop.
    )
    (4
        ; Do something else.
    )
    (else
        ; None of the above conditions held true.
    )
)
```

`(switchto <number> <conditions>)`

Works like `switch` but the numbers to compare against are implied.

```clojure
(switchto i
    (
        ; Do something if (== i 0) is true, then stop.
    )
    (
        ; Do something if (== i 1) is true, etc.
    )
    (else
        ; None of the above conditions held true.
    )
)
```

`(timer <number, string, symbol, or statement list> <delay number> [repeat]>`

Sets a timer to go off after `delay`. If the second parameter is a number, string, or symbol, a matching `alarm` event is triggered. If it's an expression list, its contents will serve as the `alarm` event. If `repeat` is given, the timer will trigger continuously in `delay` intervals.

```clojure
; Every 25 ticks, move object "one" two pixels to the right of "other" using an anonymous callback instead of an alarm.
(timer
	(
		(moverel "one" "other" 2 0)
		; Could do more things here if we wanted.
	)
	25 repeat
)
```

`(trim <string>)`

Returns the input string with all whitespace at either end removed.

```clojure
(notify (trim "  wait "))
; Displays "wait".
```

`(unmap <cel or part>)`

By the same rules as `map`, all target cels are made invisible/unmapped. Might be removed in favor of a `bool` on `map`, similar to `ghost`.

