import unittest
from large_prime_generator.large_prime_generator import LargePrimeGenerator, LargePrimeGeneratorException


class LargePrimeGeneratorTests(unittest.TestCase):
    large_prime_generator = LargePrimeGenerator()

    def test_generate_large_prime_method_throws_exception_on_too_short_numbers(self):
        self.assertRaises(LargePrimeGeneratorException, self.large_prime_generator.generate_large_prime, 5, 1000)

    def test_generate_large_prime_method_throws_exception_on_bad_range(self):
        self.assertRaises(LargePrimeGeneratorException, self.large_prime_generator.generate_large_prime, 232, 211)

    def test_rabin_miller_test_method(self):
        prime = 64063
        another_prime = 175039
        yet_another_prime = 1011749
        not_prime = 81187
        another_not_prime = 223553
        yet_another_not_prime = 1012577

        prime_is_prime = self.large_prime_generator.rabin_miller_test(prime)
        another_prime_is_prime = self.large_prime_generator.rabin_miller_test(another_prime)
        yet_another_prime_is_prime = self.large_prime_generator.rabin_miller_test(yet_another_prime)
        not_prime_is_prime = self.large_prime_generator.rabin_miller_test(not_prime)
        another_not_prime_is_prime = self.large_prime_generator.rabin_miller_test(another_not_prime)
        yet_another_not_prime_is_prime = self.large_prime_generator.rabin_miller_test(yet_another_not_prime)

        self.assertTrue(prime_is_prime)
        self.assertTrue(another_prime_is_prime)
        self.assertTrue(yet_another_prime_is_prime)
        self.assertFalse(not_prime_is_prime)
        self.assertFalse(another_not_prime_is_prime)
        self.assertFalse(yet_another_not_prime_is_prime)

    def test_test_if_prime_method(self):
        basic_prime = 431
        not_prime = 359 * 11
        even_not_prime = 106
        not_basic_prime = 64063

        basic_prime_is_prime = self.large_prime_generator.test_if_prime(basic_prime)
        not_prime_is_prime = self.large_prime_generator.test_if_prime(not_prime)
        even_not_prime_is_prime = self.large_prime_generator.test_if_prime(even_not_prime)
        not_basic_prime_is_prime = self.large_prime_generator.test_if_prime(not_basic_prime)

        self.assertTrue(basic_prime_is_prime)
        self.assertFalse(not_prime_is_prime)
        self.assertFalse(even_not_prime_is_prime)
        self.assertTrue(not_basic_prime_is_prime)

    def test_generate_large_prime_method(self):
        first_prime = self.large_prime_generator.generate_large_prime(100, 200)
        second_prime = self.large_prime_generator.generate_large_prime(512, 1024)
        third_prime = self.large_prime_generator.generate_large_prime(256, 512)
        first_prime_is_prime = self.large_prime_generator.test_if_prime(first_prime)
        second_prime_is_prime = self.large_prime_generator.test_if_prime(second_prime)
        third_prime_is_prime = self.large_prime_generator.test_if_prime(third_prime)

        self.assertTrue(first_prime_is_prime)
        self.assertTrue(second_prime_is_prime)
        self.assertTrue(third_prime_is_prime)
