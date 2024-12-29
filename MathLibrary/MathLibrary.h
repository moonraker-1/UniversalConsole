#pragma once

using namespace System;

namespace MathLibrary {
    template <typename T>
    public ref class Calculator {
    public:
        // Method declarations
        static T Mod(const T& a, const T& b);
        static T Power(const T& base, const T& exp);
        static T Exp(const T& value);
        static T SquareRoot(const T& value);
        static T Sine(const T& angle);
        static T Cosine(const T& angle);
        static T Tangent(const T& angle);
        static T Cotangent(const T& angle);

        Calculator();

    };
}

