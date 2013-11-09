import unittest
from stop_criteria.stop_criteria import StopCriteria


class StopCriteriaTests(unittest.TestCase):
    def test_should_stop_method_on_maximum_iterations(self):
        stop_criteria = StopCriteria(5, 3)

        should_stop_before_stop = stop_criteria.should_stop()
        stop_criteria.next_iteration(1)
        should_stop_after_first_iteration = stop_criteria.should_stop()
        stop_criteria.next_iteration(1)
        should_stop_after_second_iteration = stop_criteria.should_stop()
        stop_criteria.next_iteration(2)
        should_stop_after_third_iteration = stop_criteria.should_stop()
        stop_criteria.next_iteration(3)
        should_stop_after_fourth_iteration = stop_criteria.should_stop()
        stop_criteria.next_iteration(3)
        should_stop_after_fifth_iteration = stop_criteria.should_stop()

        self.assertFalse(should_stop_before_stop)
        self.assertFalse(should_stop_after_first_iteration)
        self.assertFalse(should_stop_after_second_iteration)
        self.assertFalse(should_stop_after_third_iteration)
        self.assertFalse(should_stop_after_fourth_iteration)
        self.assertTrue(should_stop_after_fifth_iteration)

    def test_should_stop_method_on_maximum_iterations_without_score_change(self):
        stop_criteria = StopCriteria(5, 3)

        should_stop_before_stop = stop_criteria.should_stop()
        stop_criteria.next_iteration(1)
        should_stop_after_first_iteration = stop_criteria.should_stop()
        stop_criteria.next_iteration(1)
        should_stop_after_second_iteration = stop_criteria.should_stop()
        stop_criteria.next_iteration(1)
        should_stop_after_third_iteration = stop_criteria.should_stop()

        self.assertFalse(should_stop_before_stop)
        self.assertFalse(should_stop_after_first_iteration)
        self.assertFalse(should_stop_after_second_iteration)
        self.assertTrue(should_stop_after_third_iteration)
