#include "pch.h"
#include "MathLibrary.h"

namespace MathLibrary {
    
    template <typename T>
    Calculator<T>::Calculator() {}

    template <typename T>
    T Calculator<T>::Mod(const T& a, const T& b) {
        static_assert(std::is_integral<T>::value, "Modulus is only supported for integer types.");
        if (b == 0) {
            throw std::invalid_argument("Division by zero is not allowed.");
        }
        return a % b;
    }

    template <typename T>
    T Calculator<T>::Power(const T& base, const T& exp) {
        return std::pow(base, exp);
    }

    template <typename T>
    T Calculator<T>::Exp(const T& value) {
        return std::exp(value);
    }

    template <typename T>
    T Calculator<T>::SquareRoot(const T& value) {
        if (value < 0) {
            throw std::invalid_argument("Square root of a negative number is not allowed.");
        }
        return std::sqrt(value);
    }



    // Sine
    template <typename T>
    T Calculator<T>::Sine(const T& angle) {
        return std::sin(angle);
    }

    // Cosine
    template <typename T>
    T Calculator<T>::Cosine(const T& angle) {
        return std::cos(angle);
    }

    // Tangent
    template <typename T>
    T Calculator<T>::Tangent(const T& angle) {
        return std::tan(angle);
    }

    // Cotangent
    template <typename T>
    T Calculator<T>::Cotangent(const T& angle) {
        T tanValue = std::tan(angle);
        if (tanValue == 0) {
            throw std::invalid_argument("Cotangent is undefined for this angle.");
        }
        return 1 / tanValue;
    }
}
