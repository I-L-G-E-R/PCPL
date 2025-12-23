# test_rk1.py

import unittest
from RK1_refactored import (
    Student, Group, StudentGroup,
    get_one_to_many, get_many_to_many,
    task_d1, task_d2, task_d3
)


class TestRK1(unittest.TestCase):

    def setUp(self):
        """Подготовка данных для всех тестов"""
        self.groups = [
            Group(1, 'ИУ5-31Б'),
            Group(2, 'ИУ5-32Б'),
            Group(3, 'АК4-01Б'),
            Group(11, 'АЭ7-11М'),
        ]
        self.students = [
            Student(1, 'Петров', 2500, 1),
            Student(2, 'Сидоров', 3000, 2),
            Student(3, 'Иванов', 3500, 2),
            Student(4, 'Смирнов', 2500, 3),
            Student(5, 'Кузнецов', 4000, 3),
        ]
        self.students_groups = [
            StudentGroup(1, 1),
            StudentGroup(2, 2),
            StudentGroup(3, 2),
            StudentGroup(4, 3),
            StudentGroup(5, 3),
            StudentGroup(1, 11),
            StudentGroup(2, 11),
            StudentGroup(4, 11),
        ]

        self.one_to_many = get_one_to_many(self.students, self.groups)
        self.many_to_many = get_many_to_many(self.students, self.groups, self.students_groups)

    def test_task_d1(self):
        """Тест задания Д1: фамилии на 'ов'"""
        result = task_d1(self.one_to_many)
        expected = [('Петров', 'ИУ5-31Б'), ('Сидоров', 'ИУ5-32Б'), ('Иванов', 'ИУ5-32Б'), ('Смирнов', 'АК4-01Б')]
        self.assertEqual(result, expected)

    def test_task_d2(self):
        """Тест задания Д2: средняя стипендия по группам"""
        result = task_d2(self.one_to_many, self.groups)
        expected = [
            ('ИУ5-31Б', 2500.0),
            ('ИУ5-32Б', 3250.0),
            ('АК4-01Б', 3250.0),
        ]
        self.assertEqual(result, expected)

    def test_task_d3(self):
        """Тест задания Д3: группы на 'А' и их студенты"""
        result = task_d3(self.many_to_many, self.groups)
        expected = {
            'АК4-01Б': ['Смирнов', 'Кузнецов'],
            'АЭ7-11М': ['Петров', 'Сидоров', 'Смирнов']
        }
        self.assertEqual(result, expected)


if __name__ == '__main__':
    unittest.main()