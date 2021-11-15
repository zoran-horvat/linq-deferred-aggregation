# Deferred Aggregation
Library with LINQ extensions that allow aggregating sequences while consuming them.

The goal of this library is to define an operator with deferred execution which accumulates values from a sequence as it is being consumed. However, this new operator itself should not cause the sequence to be consumed, but only accumulate values and present them along with the result of a subsequent call to a method with immediate execution.

Here are the precise requirements. Deferred aggregate operator satisfies the following statements:

1. It applies to any sequence (`IEnumerable<T>`)
2. It does not cause sequence to be iterated
3. It returns an object to which all existing LINQ operators apply
4. Its result contains an aggregated value which is, at any moment, a function of sequence elements that were iterated up to that moment
5. Repeated execution on the same `IEnumerable<T>` object yields the same result

In other words, the new aggregate operator should operate similarly to all other operators with deferred execution belonging to the LINQ library.
